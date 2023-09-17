using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using System;

public class Bomb : ElympicsMonoBehaviour, IUpdatable, IRemovable
{
    [SerializeField] private float range;
    [SerializeField] private float timeToBoom;
    [SerializeField] private float force = 100.0f;
    [SerializeField] private float timeToDestroy = 2.0f;

    [Header("References")]
    [SerializeField] private GameObject bombModel;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioSource audioSource;

    public ElympicsFloat currentBombTime = new ElympicsFloat();
    public ElympicsFloat currentTimeToDestroy = new ElympicsFloat();

    public ElympicsBool markedToDestroy = new ElympicsBool();
    public ElympicsBool explode = new ElympicsBool();


    private void Awake()
    {
        explode.ValueChanged += PlayExplosionParticles;
    }

    public void ElympicsUpdate()
    {
        if (markedToDestroy.Value)
        {
            currentTimeToDestroy.Value += Elympics.TickDuration;

            if(currentTimeToDestroy.Value >= timeToDestroy)
                ElympicsDestroy(this.gameObject);

        }
        else
        {
            currentBombTime.Value += Elympics.TickDuration;

            if (currentBombTime.Value >= timeToBoom)
            {
                Explode();
            }
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
        markedToDestroy.Value = true;
        currentBombTime.Value = 0.0f;
        explode.Value = true;
    }

    private void PlayExplosionParticles(bool lastValue, bool newValue)
    {
        if (newValue)
        {
            audioSource.Play();
            explosionParticles.Play();
        }
    }
}
