using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SlowEffectArea : ElympicsMonoBehaviour, IUpdatable, IRemovable
{
    [SerializeField] private float activeTime = 0.0f;

    protected ElympicsBool markedToDestroy = new ElympicsBool();
    protected ElympicsFloat currentTime = new ElympicsFloat();

    private ElympicsGameObject owner = new ElympicsGameObject();
    private SkillEffect effect;

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

    public void SetOwner(ElympicsBehaviour owner)
    {
        this.owner.Value = owner;
    }

    public void SetEffect(SkillEffect effect)
    {
        this.effect = effect;
    }

    public void Remove()
    {
        markedToDestroy.Value = true;
    }

    public void ElympicsUpdate()
    {
        if(markedToDestroy.Value)
        {
            ElympicsDestroy(this.gameObject);
        }

        currentTime.Value += Elympics.TickDuration;

        if (currentTime.Value >= activeTime)
            markedToDestroy.Value = true;
    }
}
