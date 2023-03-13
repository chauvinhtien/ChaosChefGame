using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;
    [SerializeField] protected Transform spwanPosition;

    protected KitchenObject kitchenObject;

    public virtual void Interact(PlayerController playerController)
    {
        Debug.LogError("Base couter.Interact()");
    }
    public virtual void InteractAlternate(PlayerController player)
    {
        Debug.LogError("Base couter.InteractAlternate");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return spwanPosition;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitChenObject()
    {
        return kitchenObject != null;
    }
}
