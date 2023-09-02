using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.Serialization;

public abstract class ASkill : ElympicsMonoBehaviour, IInitializable, IUpdatable
{
    [SerializeField] private Sprite skillImage;
    [SerializeField] private int skillID;
    [SerializeField] protected float fireRate = 60.0f;
    [SerializeField] protected int skillCharges = 1;
    [SerializeField] protected SkillIndicator indicator;
    [SerializeField] protected SkillEffect effect;

    public ElympicsFloat TimeRatio { get; private set; } = new ElympicsFloat();
    public float TimeBetweenShots => timeBetweenShots;
    public GameObject Owner => transform.root.gameObject;
    public SkillIndicator Indicator => indicator;
    public int SkillID => skillID;
    public bool HasCharges => skillCurrentCharges.Value > 0;
    public int Charges => skillCurrentCharges.Value;
    public Sprite SkillImage => skillImage;

    public ElympicsFloat CurrentTimeBetweenShots = new ElympicsFloat(0.0f);
    protected float timeBetweenShots = 0.0f;
    protected bool isReady => CurrentTimeBetweenShots.Value >= timeBetweenShots;
    protected ElympicsInt skillCurrentCharges = new ElympicsInt();

    #region IInitializable
    public void Initialize()
    {
        CalculateTimeBetweenShots();
        ResetCharges();
    }
    #endregion

    #region IUpdatable
    public virtual void ElympicsUpdate()
    {
        if (!isReady)
        {
            CurrentTimeBetweenShots.Value += Elympics.TickDuration;
        }
        TimeRatio.Value = CurrentTimeBetweenShots.Value / timeBetweenShots;
    }
    #endregion

    public bool TryPerformPrimaryAction()
    {
        return ExecutePrimaryActionIfReady();
    }

    public void ResetCharges()
    {
        skillCurrentCharges.Value = skillCharges;
    }

    public abstract void UpdateAimPosition(Vector3 worldPosition);

    protected abstract void ProcessSkillAction();

    private bool ExecutePrimaryActionIfReady()
    {
        bool result = isReady;
        
        if(isReady)
        {
            ProcessSkillAction();
            CurrentTimeBetweenShots.Value = 0.0f;
            skillCurrentCharges.Value--;
        }

        return result;
    }

    private void CalculateTimeBetweenShots()
    {
        if(fireRate > 0)
        {
            timeBetweenShots = 60.0f / fireRate;
        }
        else
        {
            timeBetweenShots = 0.0f;
        }
    }
}
