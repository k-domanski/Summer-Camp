using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Bomb : ElympicsMonoBehaviour,IUpdatable
{
    [SerializeField] private float range;
    [SerializeField] private float timeToBoom;
    [SerializeField] private float force = 100.0f;

    public ElympicsFloat currentBombTime = new ElympicsFloat();
    public ElympicsBool markedToDestroy = new ElympicsBool();

    public void ElympicsUpdate()
    {
        if (markedToDestroy.Value)
            ElympicsDestroy(this.gameObject);

        currentBombTime.Value += Elympics.TickDuration;

        if(currentBombTime.Value >= timeToBoom)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in objectsInRange)
        {
            if (collider.transform.root.gameObject.TryGetComponent<PlayerData>(out var player))
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);

                //player.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, range, 0.0f, ForceMode.Impulse);
            }
        }
        markedToDestroy.Value = true;
        currentBombTime.Value = 0.0f;
    }
}
