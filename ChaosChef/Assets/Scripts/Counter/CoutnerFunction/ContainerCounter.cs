using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private SO_KitchenObject kitchenObjectData;
    public event EventHandler OnPlayerGrabbedObject;


    public override void Interact(PlayerController player)
    {
        if(!player.HasKitChenObject())
        {
            KitchenObject.SpawnKitChenObject(kitchenObjectData, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }else
        {
            //Player Already have kitchenObject
        }
    }
    
}
