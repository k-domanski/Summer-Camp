using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputProvider : MonoBehaviour
{
    public Vector2 Movement { get; private set; }
    public Vector2 MousePos { get; private set; }
    public bool FireSkill { get; private set; }
    public bool FireSecondarySkill { get; private set; }
    public bool ActivePrimarySkill { get; private set; }
    public bool ActiveSecondarySkill { get; private set; }

    private MainControls mainControls;

    private void OnEnable()
    {
        mainControls = new MainControls();
        mainControls.Player.Enable();
        SubscribeToPlayerActions();
        mainControls.Player.ReturnToMenu.performed += OnReturnToMenu;
    }

    private void OnDestroy()
    {
        mainControls.Player.Disable();
        UnsubscribeFromPlayerActions();
        mainControls.Player.ReturnToMenu.started -= OnReturnToMenu;
    }

    public void DisableInput()
    {
        //mainControls.Player.Disable();
        UnsubscribeFromPlayerActions();
        Movement = Vector2.zero;
        // Switch to UI Action Map ?
    }

    private void SubscribeToPlayerActions()
    {
        mainControls.Player.Move.performed += OnMove;
        mainControls.Player.Move.canceled += OnMove;
        mainControls.Player.Fire.started += OnFire;
        mainControls.Player.Fire.canceled += OnFire;
        mainControls.Player.Skill.started += OnSkill;
        mainControls.Player.Skill.canceled += OnSkill;
        mainControls.Player.Attack.started += OnAttack;
        mainControls.Player.Attack.canceled += OnAttack;
        mainControls.Player.MousePos.performed += OnMouseMove;
    }

    private void UnsubscribeFromPlayerActions()
    {
        mainControls.Player.Move.performed -= OnMove;
        mainControls.Player.Move.canceled -= OnMove;
        mainControls.Player.Fire.started -= OnFire;
        mainControls.Player.Fire.canceled -= OnFire;
        mainControls.Player.Skill.started -= OnSkill;
        mainControls.Player.Skill.canceled -= OnSkill;
        mainControls.Player.Attack.started -= OnAttack;
        mainControls.Player.Attack.canceled -= OnAttack;
        mainControls.Player.MousePos.performed -= OnMouseMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        FireSkill = context.started ? true : false;
        FireSkill = context.canceled ? false : true;
    }

    private void OnSkill(InputAction.CallbackContext context)
    {
        ActivePrimarySkill = context.started ? true : false;
        ActivePrimarySkill = context.canceled ? false : true;
    }
    
    private void OnAttack(InputAction.CallbackContext context)
    {
        ActiveSecondarySkill = context.started ? true : false;
        ActiveSecondarySkill = context.canceled ? false : true;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    private void OnReturnToMenu(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
    }
}
