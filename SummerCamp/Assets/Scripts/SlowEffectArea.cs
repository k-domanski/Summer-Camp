using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SlowEffectArea : MonoBehaviour
{
    private ElympicsGameObject owner = new ElympicsGameObject();
    private float slowAmount = 0.0f;


    public void SetOwner(ElympicsBehaviour owner)
    {
        this.owner.Value = owner;
    }

    public void SetSlowAmount(float amount)
    {
        slowAmount = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner.Value == null)
            return;

        if (other.transform.root.gameObject == owner.Value.gameObject)
            return;

        if(other.TryGetComponent<MovementController>(out MovementController movementController))
        {
            movementController.ApplySlow(slowAmount);
        }
    }
}
