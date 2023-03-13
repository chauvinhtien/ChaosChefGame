using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashed;
    public override void Interact(PlayerController playerController)
    {
        if(playerController.HasKitChenObject())
        {
            playerController.GetKitchenObject().DestroySelf();
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
