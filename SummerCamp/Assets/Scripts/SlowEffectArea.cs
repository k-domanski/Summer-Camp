using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SlowEffectArea : MonoBehaviour
{
    private ElympicsGameObject owner = new ElympicsGameObject();
    private SkillEffect effect;

    public void SetOwner(ElympicsBehaviour owner)
    {
        this.owner.Value = owner;
    }

    public void SetEffect(SkillEffect effect)
    {
        this.effect = effect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner.Value == null)
            return;

        if (other.transform.root.gameObject == owner.Value.gameObject)
            return;

        if(other.TryGetComponent<EffectController>(out var effectController))
        {
            effectController.ApplyEffect(effect);
        }    
    }
}
