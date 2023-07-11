using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public Vector2 Movement => movement;

    private Vector2 movement;

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}
