using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerController playerController)
    {
        if(playerController.HasKitChenObject())
        {
            playerController.GetKitchenObject().DestroySelf();
        }
    }
}
