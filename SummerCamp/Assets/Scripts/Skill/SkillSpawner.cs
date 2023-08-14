using System;
using Elympics;
using UnityEngine;
using UnityEngine.UI;

public class SkillSpawner : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private Image spawnIndicator;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private float cooldownTime;

    public int SkillID { get; private set; }
    public ElympicsFloat TimeToSpawn { get; private set; } = new ElympicsFloat();

    private void Start()
    {
        if (Elympics.IsServer)
        {
            TimeToSpawn.Value = cooldownTime;
        }
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

    private void Pick()
    {
        TimeToSpawn.Value = cooldownTime;
    }
}
