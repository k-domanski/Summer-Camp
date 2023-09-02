using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

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

    protected ElympicsFloat currentTimeBetweenShots = new ElympicsFloat(0.0f);
    protected float timeBetweenShots = 0.0f;
    protected bool isReady => currentTimeBetweenShots.Value >= timeBetweenShots;
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
            currentTimeBetweenShots.Value += Elympics.TickDuration;
        }
        TimeRatio.Value = currentTimeBetweenShots.Value / timeBetweenShots;
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
            currentTimeBetweenShots.Value = 0.0f;
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
