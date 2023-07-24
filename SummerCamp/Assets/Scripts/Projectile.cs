using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class Projectile : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody rb;
    
    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
    
}
