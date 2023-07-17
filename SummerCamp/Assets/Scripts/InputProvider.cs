using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public Vector2 Movement => movement;

    private Vector2 movement;

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

    private void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
