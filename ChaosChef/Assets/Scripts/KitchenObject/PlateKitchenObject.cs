using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public SO_KitchenObject kitchenObjectSO;
    }

    [SerializeField] private List<SO_KitchenObject> validKitchenObjectSoList;

    private List<SO_KitchenObject> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<SO_KitchenObject>();
    }
    public bool TryAddIngredient(SO_KitchenObject kitchenObjectSO)
    {
        if(!validKitchenObjectSoList.Contains(kitchenObjectSO))
        {
            //Not valid ingredient
            return false;
        }
        else if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<SO_KitchenObject> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
