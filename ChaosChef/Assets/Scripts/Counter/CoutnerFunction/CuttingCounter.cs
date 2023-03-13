using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private SO_CuttingRecipe[] cuttingRecipesDataArray;

    private int cuttingProgess;
    public override void Interact(PlayerController player)
    {
        if(!HasKitChenObject())
        {
            //There is no kitchenObject here
            if(player.HasKitChenObject())
            {
                //Player is carrying something
                if(HasCuttableObject(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    cuttingProgess = 0;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    SO_CuttingRecipe cuttingRecipe = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgess / cuttingRecipe.cutTimes
                    });
                }
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
                }
                
            }else
            {
                //player not carrying anything
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
           
        if(HasKitChenObject() && HasCuttableObject(GetKitchenObject().GetKitchenObjectSO()))
        {
            Debug.Log("InteracAlternate");
        
            SO_KitchenObject outputKitchenObjetSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            cuttingProgess ++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            SO_CuttingRecipe cuttingRecipe = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgess / cuttingRecipe.cutTimes
            });

            if(cuttingProgess >= GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()).cutTimes)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitChenObject(outputKitchenObjetSO, this);
                
            }
        }else
        {
            Debug.Log("Cannot cut");
        }
    }

    private bool HasCuttableObject(SO_KitchenObject holdingObject)
    {
        foreach (SO_CuttingRecipe cuttingRecipe in cuttingRecipesDataArray)
        {
            if(cuttingRecipe.input == holdingObject)
            {
                return true;
            }
        }
        return false;
    }
    private SO_KitchenObject GetOutputForInput(SO_KitchenObject inputKitChenObjectSO)
    {
        foreach (SO_CuttingRecipe cuttingRecipe in cuttingRecipesDataArray)
        {
            if(cuttingRecipe.input == inputKitChenObjectSO)
            {
                return cuttingRecipe.output;
            }
        }
        return null;
    }
    private SO_CuttingRecipe GetCuttingRecipeWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_CuttingRecipe cuttingRecipe in cuttingRecipesDataArray)
        {
            if(cuttingRecipe.input == inputKitchenObjectSO)
            {
                return cuttingRecipe;
            }
        }
        return null;
    }
}
