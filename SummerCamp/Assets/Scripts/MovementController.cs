using UnityEngine;
using Elympics;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration;
    [SerializeField]
    private Animator characterAnimation;
    
    private Rigidbody rb = null;
    private PlayerData playerData;
    
    protected ElympicsVector3 movementdirection = new ElympicsVector3();
    protected ElympicsVector3 lookAt = new ElympicsVector3();
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        if (Elympics.IsClient)
        {
            movementdirection.ValueChanged += OnDirectionChanged;
            lookAt.ValueChanged += OnLookAtChanged;
        }
    }
    public void ElympicsUpdate()
    {
    }

    private void OnDestroy()
    {
        if (Elympics.IsClient)
        {
            movementdirection.ValueChanged -= OnDirectionChanged;
            lookAt.ValueChanged -= OnLookAtChanged;
        }
    }
    
    private void OnLookAtChanged(Vector3 lastvalue, Vector3 newvalue)
    {
        characterAnimation.transform.LookAt(newvalue);
    }
    
    private void OnDirectionChanged(Vector3 lastvalue, Vector3 newvalue)
    {
        characterAnimation.SetBool("Forward",newvalue.x > 0 );
        characterAnimation.SetBool("Backward",newvalue.x < 0 );
        characterAnimation.SetBool("Left",newvalue.z > 0 );
        characterAnimation.SetBool("Right",newvalue.z < 0 );
    }

    public void ProcessInput(Vector3 movement, Vector3 rotation)
    {
        Vector3 movementDirection = movement != Vector3.zero ? this.transform.TransformDirection(movement.normalized) : Vector3.zero;

        ApplyMovement(movementDirection);
        ApplyRotation(rotation);
    }

    private void ApplyMovement(Vector3 movementDirection)
    {
        movementdirection.Value = movementDirection; 
        Vector3 defaultVelocity = movementDirection * movementSpeed;
        Vector3 fixedVelocity = Vector3.MoveTowards(rb.velocity, defaultVelocity, Elympics.TickDuration * acceleration);

        rb.velocity = new Vector3(fixedVelocity.x, rb.velocity.y, fixedVelocity.z);
    }

    private void ApplyRotation(Vector3 rotation)
    {
        lookAt.Value = rotation;
        characterAnimation.transform.LookAt(rotation);
    }
}
