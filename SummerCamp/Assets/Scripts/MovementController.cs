using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;
    [SerializeField]
    private Animator characterAnimation;
    
    private Rigidbody rb = null;
    private float baseMovementSpeed = 0.0f;

    protected ElympicsVector3 lookAt = new ElympicsVector3();
    protected ElympicsVector3 movement = new ElympicsVector3();
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseMovementSpeed = movementSpeed;
        if (Elympics.IsClient)
        {
            lookAt.ValueChanged += OnLookAtChanged;
        }
    }

    private void OnDestroy()
    {
        if (Elympics.IsClient)
        {
            lookAt.ValueChanged -= OnLookAtChanged;
        }
    }

    public void ApplySlow(float amount)
    {
        movementSpeed = amount;
    }

    public void ResetSpeed()
    {
        movementSpeed = baseMovementSpeed;
    }

    private void OnLookAtChanged(Vector3 lastvalue, Vector3 newvalue)
    {
        characterAnimation.transform.LookAt(newvalue);

        var forwardDir = characterAnimation.transform.forward;

        float angle = Vector3.SignedAngle(movement.Value, forwardDir, Vector3.down);
        //Debug.Log($"MoveDir { movement.Value} | LookDir {forwardDir} | Angle: {angle}");
        characterAnimation.SetBool("Moving",movement.Value.magnitude > 0);
        characterAnimation.SetFloat("MoveAngle",angle);
    }

    public void ProcessInput(Vector3 movement, Vector3 rotation)
    {
        Vector3 direction = movement != Vector3.zero ? this.transform.TransformDirection(movement.normalized) : Vector3.zero;

        ApplyMovement(direction);
        ApplyRotation(rotation);
    }

    private void ApplyMovement(Vector3 direction)
    {
        movement.Value = direction;
        Vector3 defaultVelocity = direction * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb.velocity = new Vector3(fixedVelocity.x, rb.velocity.y, fixedVelocity.z);
    }

    private void ApplyRotation(Vector3 rotation)
    {
        lookAt.Value = rotation;
        characterAnimation.transform.LookAt(rotation);
    }
}
