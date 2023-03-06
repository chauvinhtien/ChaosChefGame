using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

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
