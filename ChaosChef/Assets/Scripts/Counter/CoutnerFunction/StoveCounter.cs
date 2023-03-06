using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    private enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;

    [SerializeField] private SO_StoveRecipe[] stoveRecipeSOArray;
    [SerializeField] private SO_BurnRecipe[] burnRecipeSOArray;

    private float fryingTimer;
    private float buringTimer;

    private SO_BurnRecipe burnRecipeSO;
    private SO_StoveRecipe stoveRecipeSO;
    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        switch(state)
        {
            case State.Idle:
                break;
            case State.Frying:
                
                fryingTimer += Time.deltaTime;
                
                if(fryingTimer > stoveRecipeSO.stoveTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitChenObject(stoveRecipeSO.output, this);
                    Debug.Log("Meat cooked");

                    
                    state = State.Fried;
                    buringTimer = 0f;

                    burnRecipeSO = GetBurnRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                }
                
                break;
            case State.Fried:

                buringTimer += Time.deltaTime;
                
                if(buringTimer > burnRecipeSO.burnedTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitChenObject(burnRecipeSO.output, this);
                    Debug.Log("Meat Burned");
                    state = State.Burned;
                }
                
                break;
            case State.Burned:
                break;
        }
        Debug.Log(state);
    }

    public override void Interact(PlayerController player)
    {
        if(!HasKitChenObject())
        {
            //There is no kitchenObject here
            if(player.HasKitChenObject())
            {
                //Player is carrying something
                if(HasStovableObject(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    stoveRecipeSO = GetStoveRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    state = State.Frying;
                    fryingTimer = 0f;
                }
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

                state = State.Idle;
            }
        }
    }

    private bool HasStovableObject(SO_KitchenObject holdingObject)
    {
        foreach (SO_StoveRecipe stoveRecipe in stoveRecipeSOArray)
        {
            if(stoveRecipe.input == holdingObject)
            {
                return true;
            }
        }
        return false;
    }
    private SO_KitchenObject GetOutputForInput(SO_KitchenObject inputKitChenObjectSO)
    {
        foreach (SO_StoveRecipe stoveRecipe in stoveRecipeSOArray)
        {
            if(stoveRecipe.input == inputKitChenObjectSO)
            {
                return stoveRecipe.output;
            }
        }
        return null;
    }
    private SO_StoveRecipe GetStoveRecipeWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_StoveRecipe stoveRecipe in stoveRecipeSOArray)
        {
            if(stoveRecipe.input == inputKitchenObjectSO)
            {
                return stoveRecipe;
            }
        }
        return null;
    }

    private SO_BurnRecipe GetBurnRecipeWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_BurnRecipe burnRecipe in burnRecipeSOArray)
        {
            if(burnRecipe.input == inputKitchenObjectSO)
            {
                return burnRecipe;
            }
        }
        return null;
    }
}
