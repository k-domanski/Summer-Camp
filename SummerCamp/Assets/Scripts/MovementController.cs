using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;

    private Rigidbody rb = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ProcessMovement(float horizontal, float vertical)
    {
        Vector3 inputVector = new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = inputVector != Vector3.zero ? this.transform.TransformDirection(inputVector.normalized) : Vector3.zero;

        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector3 movementDirection)
    {
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb.velocity = new Vector3(fixedVelocity.x, rb.velocity.y, fixedVelocity.z);
    }
}
