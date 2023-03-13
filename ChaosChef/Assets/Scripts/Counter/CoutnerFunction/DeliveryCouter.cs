using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCouter : BaseCounter
{
    public static DeliveryCouter Instance {get; private set;}
    private void Awake() {
        Instance = this;
    }
    public override void Interact(PlayerController player)
    {
        if(player.HasKitChenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliveRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
