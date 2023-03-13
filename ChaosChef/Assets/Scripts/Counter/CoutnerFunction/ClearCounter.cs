using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
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
            }
        }else{
            if(player.HasKitChenObject())
            {
                //Player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }else
                {
                    //Player is not carrying a plate but sth else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Couter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else
            {
                //player not carrying anything
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
