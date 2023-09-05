using UnityEngine;
using Elympics;
using UnityEngine.Serialization;

public abstract class ASkill : ElympicsMonoBehaviour, IInitializable, IUpdatable
{
    [SerializeField] private Sprite skillImage;
    [SerializeField] private int skillID;
    [SerializeField] protected float skillCooldownSeconds = 2.25f;
    [SerializeField] protected int skillCharges = 1;
    [SerializeField] protected SkillIndicator indicator;
    [SerializeField] protected SkillEffect effect;
    
    public GameObject Owner => transform.root.gameObject;
    public SkillIndicator Indicator => indicator;
    public int SkillID => skillID;
    public bool HasCharges => skillCurrentCharges.Value > 0;
    public int Charges => skillCurrentCharges.Value;
    public Sprite SkillImage => skillImage; 
    protected bool isReady => CurrentTimeBetweenShots.Value >= skillCooldownSeconds;

    public float TimeRatio => CurrentTimeBetweenShots.Value / skillCooldownSeconds;
    public ElympicsFloat CurrentTimeBetweenShots = new ElympicsFloat(0.0f);
    protected ElympicsInt skillCurrentCharges = new ElympicsInt();

    protected bool canUseSkill = false;

    #region IInitializable
    public void Initialize()
    {
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
        
        if(isReady && canUseSkill)
        {
            ProcessSkillAction();
            CurrentTimeBetweenShots.Value = 0.0f;
            skillCurrentCharges.Value--;
        }

        return result;
    }
}
