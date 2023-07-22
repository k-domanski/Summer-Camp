using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 MousePos { get; private set; }

    private MainControls mainControls;

    private void OnEnable()
    {
        mainControls = new MainControls();
        mainControls.Player.Enable();
        mainControls.Player.Move.performed += OnMove;
        mainControls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        mainControls.Player.Disable();
        mainControls.Player.Move.performed -= OnMove;
        mainControls.Player.Move.canceled -= OnMove;
    }

    public void DisableInput()
    {
        mainControls.Player.Disable();

        // Switch to UI Action Map ?
    }

    private void FixedUpdate()
    {
        MousePos = Mouse.current.position.ReadValue();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }
}
