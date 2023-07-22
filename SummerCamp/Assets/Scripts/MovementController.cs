using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Camera playerCamera;
    
    [SerializeField]
    private Animator characterAnimation;
    
    private Rigidbody rb = null;

    private ElympicsVector3 movementdirection = new ElympicsVector3();
    private ElympicsVector3 lookAt = new ElympicsVector3();
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (Elympics.IsClient)
        {
            movementdirection.ValueChanged += OnDirectionChanged;
            lookAt.ValueChanged += SetLookAt;
        }
    }

    public void ElympicsUpdate()
    {
        if (Elympics.IsClient)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var worldPos = playerCamera.ScreenToWorldPoint(mousePos);
            worldPos.y = transform.position.y;
            
            Debug.Log($"{mousePos} | {worldPos}");
            lookAt.Value = worldPos;
        }
        
    }

    private void SetLookAt(Vector3 lastvalue, Vector3 newvalue)
    {
        characterAnimation.transform.LookAt(newvalue);
    }

    private void OnDestroy()
    {
        if (Elympics.IsClient)
        {
            movementdirection.ValueChanged -= OnDirectionChanged;
            lookAt.ValueChanged -= SetLookAt;
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
        movementdirection.Value = movementDirection; 
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb.velocity = new Vector3(fixedVelocity.x, rb.velocity.y, fixedVelocity.z);
    }
}
