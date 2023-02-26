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
            Transform kitchenObjectTransform = Instantiate(kitchenObjectData.prepab, spwanPosition);
            //Give the Kitchen Object to Player
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }else
        {
            Debug.LogError("player Already have kitchenObject");
        }
        
        
        
    }
}
