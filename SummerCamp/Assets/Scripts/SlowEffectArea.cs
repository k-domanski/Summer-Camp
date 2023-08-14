using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class SlowEffectArea : MonoBehaviour
{
    private ElympicsGameObject owner = new ElympicsGameObject();


    public void SetOwner(ElympicsBehaviour owner)
    {
        this.owner.Value = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner.Value == null)
            return;

        if (other.transform.root.gameObject == owner.Value.gameObject)
            return;

        if(other.TryGetComponent<MovementController>(out MovementController movementController))
        {
            movementController.ApplySlow();
        }
    }
}
