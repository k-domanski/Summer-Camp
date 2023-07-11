using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;

    private Rigidbody rigidbody = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ProcessMovement(float vertical, float horizontal)
    {
        Vector3 inputVector = new Vector3(horizontal, vertical, 0);
        Vector3 movementDirection = inputVector != Vector3.zero ? this.transform.TransformDirection(inputVector.normalized) : Vector3.zero;

        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector3 movementDirection)
    {
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rigidbody.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rigidbody.velocity = new Vector3(fixedVelocity.x, fixedVelocity.y, rigidbody.velocity.z);
    }
}
