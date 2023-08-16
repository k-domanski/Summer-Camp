using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectController : ElympicsMonoBehaviour, IUpdatable, IInitializable
{
    [SerializeField] private Volume volumeProfile;
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
            volumeProfile.weight = 1.0f;
            if(currentDuration.Value > skillEffect.duration)
            {
                skillEffect = null;
                currentDuration.Value = 0.0f;
                volumeProfile.weight = 0.0f;
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
