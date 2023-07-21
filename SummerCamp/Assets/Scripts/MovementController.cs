using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;
    [SerializeField]
    private ElympicsVector3 direction = new ElympicsVector3();
    [SerializeField]
    private Animator characterAnimation;
    
    private Rigidbody rb = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (Elympics.IsClient)
        {
            direction.ValueChanged += OnDirectionChanged;
        }
    }

    private void OnDestroy()
    {
        if (Elympics.IsClient)
        {
            direction.ValueChanged -= OnDirectionChanged;
        }
    }

    private void OnDirectionChanged(Vector3 lastvalue, Vector3 newvalue)
    {
        characterAnimation.SetBool("Forward",newvalue.x > 0 );
        characterAnimation.SetBool("Backward",newvalue.x < 0 );
        characterAnimation.SetBool("Left",newvalue.z > 0 );
        characterAnimation.SetBool("Right",newvalue.z < 0 );
    }

    public void ProcessMovement(float horizontal, float vertical)
    {
        Vector3 inputVector = new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = inputVector != Vector3.zero ? this.transform.TransformDirection(inputVector.normalized) : Vector3.zero;

        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector3 movementDirection)
    {
        direction.Value = movementDirection; 
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb.velocity = new Vector3(fixedVelocity.x, rb.velocity.y, fixedVelocity.z);
    }
}
