using System;
using Elympics;
using UnityEngine;
using UnityEngine.UI;

public class SkillSpawner : ElympicsMonoBehaviour, IUpdatable, IRemovable
{
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private SkillManager skillManager;
    [SerializeField]
    private Image spawnIndicator;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private float cooldownTime;
    
    public bool AvailableForPickUp => spawn.activeInHierarchy;

    protected ElympicsInt skillID = new ElympicsInt();
    
    public int SkillID => skillID.Value;
    public ElympicsFloat TimeToSpawn { get; private set; } = new ElympicsFloat();

    public event Action<SkillSpawner> onSkillPickedUp;

    private void Awake()
    {
        if (skillManager == null)
        {
            skillManager = FindObjectOfType<SkillManager>();
        }
    }

    private void Start()
    {
        if (Elympics.IsClient)
        {
            TimeToSpawn.ValueChanged += OnTimeChanged;
        }
    }

    private void OnTimeChanged(float lastvalue, float newvalue)
    {
        spawnIndicator.fillAmount = 1 - (newvalue / cooldownTime);
    }

    public void ElympicsUpdate()
    {
        if (manager.IsRunning.Value == false)
        {
            return;
        }
        
        if (Elympics.IsServer)
        {
            if (spawn.activeInHierarchy == false)
            {
                TimeToSpawn.Value -= Elympics.TickDuration;
                if (TimeToSpawn.Value <= 0)
                {
                    spawn.SetActive(true);
                }
            }
        }
    }

    public void Pick()
    {
        ResetSpawner();
        onSkillPickedUp?.Invoke(this);
    }

    public void ResetSpawner()
    {
        TimeToSpawn.Value = cooldownTime;
        
        if (Elympics.IsServer)
        {
            skillID.Value = skillManager.GetRandomSkillID();
        }

        spawn.SetActive(false);
    }

    public void Remove()
    {
        Pick();
    }
}
