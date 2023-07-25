using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Projectile : ElympicsMonoBehaviour, IInitializable, IUpdatable
{
    [SerializeField] private float speed = 5.0f;

    private ElympicsBool isReadyToDestroy = new ElympicsBool();
    private ElympicsGameObject owner = new ElympicsGameObject();

    private Rigidbody rb;

    public void ElympicsUpdate()
    {
        if(isReadyToDestroy.Value)
            ElympicsDestroy(this.gameObject);
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
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

        if(other.TryGetComponent<PlayerData>(out var playerData))
        {
            Debug.Log($"COLLISION WITH PLAYER: {playerData.PlayerID}");
        }

        this.isReadyToDestroy.Value = true;
    }
}
