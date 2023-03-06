using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public Vector2 RawMovementInput {get; private set;}
    public bool interactInput       {get; private set;}

    public int NorInputX {get; private set;}
    public int NorInputY {get; private set;}

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NorInputX = Mathf.RoundToInt(RawMovementInput.x);
        NorInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void OnInteractAlternateInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
