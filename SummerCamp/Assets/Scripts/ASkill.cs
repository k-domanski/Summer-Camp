using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public abstract class ASkill : ElympicsMonoBehaviour, IInitializable, IUpdatable
{
    [SerializeField] protected float fireRate = 60.0f;
    
    public float TimeBetweenShots => timeBetweenShots;
    public GameObject Owner => transform.root.gameObject;

    protected ElympicsFloat currentTimeBetweenShots = new ElympicsFloat(0.0f);
    protected float timeBetweenShots = 0.0f;
    protected bool isReady => currentTimeBetweenShots.Value >= timeBetweenShots;

    #region IInitializable
    public void Initialize()
    {
        CalculateTimeBetweenShots();
    }
    #endregion

    #region IUpdatable
    public virtual void ElympicsUpdate()
    {
        if (!isReady)
        {
            currentTimeBetweenShots.Value += Elympics.TickDuration;
        }
    }
    #endregion

    public void PerformPrimaryAction()
    {
        ExecutePrimaryActionIfReady();
    }

    protected abstract void ProcessSkillAction();

    private void ExecutePrimaryActionIfReady()
    {
        if(isReady)
        {
            ProcessSkillAction();
            currentTimeBetweenShots.Value = 0.0f;
        }
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
