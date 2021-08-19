using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;

    public bool attack1_Input;
    public bool attack2_Input;
    public bool defense_Input;

    Vector2 movementInput;

    PlayerController inputActions;

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerController();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
        }

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        HandleDefenseInput();
        HandleAttack1Input();
        HandleAttack2Input();
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    private void HandleDefenseInput()
    {
        defense_Input = inputActions.PlayerActions.Defense.phase == UnityEngine.InputSystem.InputActionPhase.Started;
    }


    private void HandleAttack1Input()
    {
        attack1_Input = inputActions.PlayerActions.Attack1.phase == UnityEngine.InputSystem.InputActionPhase.Started;
    }

    private void HandleAttack2Input()
    {
        attack2_Input = inputActions.PlayerActions.Attack2.phase == UnityEngine.InputSystem.InputActionPhase.Started;
    }
}
