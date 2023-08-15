using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class EffectController : ElympicsMonoBehaviour, IUpdatable, IInitializable
{
    private MovementController movementController;
    private SkillEffect skillEffect;
    private bool IsApplied => skillEffect != null;
    private ElympicsFloat currentDuration = new ElympicsFloat();
    
    public void ElympicsUpdate()
    {
        if(IsApplied)
        {
            movementController.ApplySlow(skillEffect.effectAmount);

            currentDuration.Value += Elympics.TickDuration;

            if(currentDuration.Value > skillEffect.duration)
            {
                skillEffect = null;
                currentDuration.Value = 0.0f;
            }
        }
        else
        {
            movementController.ResetSpeed();
        }
    }

    public void Initialize()
    {
        movementController = GetComponent<MovementController>();
    }

    public void ApplyEffect(SkillEffect effect)
    {
        skillEffect = effect;
    }
}
