using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : ElympicsMonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;

    private Rigidbody2D rb2D = null;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void ProcessMovement(float horizontal, float vertical)
    {
        Vector3 inputVector = new Vector3(horizontal, vertical, 0);
        Vector3 movementDirection = inputVector != Vector3.zero ? this.transform.TransformDirection(inputVector.normalized) : Vector3.zero;

        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector3 movementDirection)
    {
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb2D.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb2D.velocity = new Vector3(fixedVelocity.x, fixedVelocity.y);
    }
}
