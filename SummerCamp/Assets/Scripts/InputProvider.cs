using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 MousePos { get; private set; }
    public bool IsFire { get; private set; }

    private MainControls mainControls;

    private void OnEnable()
    {
        mainControls = new MainControls();
        mainControls.Player.Enable();
        mainControls.Player.Move.performed += OnMove;
        mainControls.Player.Move.canceled += OnMove;
        mainControls.Player.Fire.started += OnFire;
        mainControls.Player.Fire.canceled += OnFire;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        IsFire = context.started ? true : false;
        IsFire = context.canceled ? false : true;
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
