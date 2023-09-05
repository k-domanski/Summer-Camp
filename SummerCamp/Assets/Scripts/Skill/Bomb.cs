using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Bomb : ElympicsMonoBehaviour, IUpdatable, IRemovable
{
    [SerializeField] private float range;
    [SerializeField] private float timeToBoom;
    [SerializeField] private float force = 100.0f;
    [SerializeField] private float timeToDestroy = 2.0f;

    [Header("References")]
    [SerializeField] private GameObject bombModel;
    [SerializeField] private ParticleSystem explosionParticles;

    public ElympicsFloat currentBombTime = new ElympicsFloat();
    public ElympicsFloat currentTimeToDestroy = new ElympicsFloat();

    public ElympicsBool markedToDestroy = new ElympicsBool();

    public void ElympicsUpdate()
    {
        if (markedToDestroy.Value)
        {
            currentTimeToDestroy.Value += Elympics.TickDuration;

            if(currentBombTime.Value >= timeToDestroy)
                ElympicsDestroy(this.gameObject);

        }

        currentBombTime.Value += Elympics.TickDuration;

        if(currentBombTime.Value >= timeToBoom)
        {
            Explode();
        }
    }

    public void Remove()
    {
        markedToDestroy.Value = true;
        currentTimeToDestroy.Value = timeToDestroy;
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
        bombModel.SetActive(false);
        explosionParticles.Play();
        markedToDestroy.Value = true;
        currentBombTime.Value = 0.0f;
        currentTimeToDestroy.Value = 0.0f;
    }
}
