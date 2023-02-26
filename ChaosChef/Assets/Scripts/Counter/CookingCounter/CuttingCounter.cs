using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if(!HasKitChenObject())
        {
            //There is no kitchenObject here
            if(player.HasKitChenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else
            {
                //player not carrying anything
                Debug.LogError("player not carrying anything");
            }
        }else{
            if(player.HasKitChenObject())
            {
                //Player is carrying something
                Debug.LogError("ClearCouter already have kitchen object");
            }else
            {
                //player not carrying anything
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
