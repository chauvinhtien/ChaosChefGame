using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs: EventArgs{
        public State state;
    }

    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;

    [SerializeField] private SO_StoveRecipe[] stoveRecipeSOArray;
    [SerializeField] private SO_BurnRecipe[] burnRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;

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

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)fryingTimer / stoveRecipeSO.stoveTime
                });
                
                if(fryingTimer > stoveRecipeSO.stoveTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitChenObject(stoveRecipeSO.output, this);
            
                    state = State.Fried;
                    burningTimer = 0f;


                    burnRecipeSO = GetBurnRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = this.state
                    });
                }
                
                break;
            case State.Fried:

                burningTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)burningTimer / stoveRecipeSO.stoveTime
                });
                
                if(burningTimer > burnRecipeSO.burnedTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitChenObject(burnRecipeSO.output, this);
                    

                    state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = this.state
                    });
                    
                }
                break;
            case State.Burned:
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                
                break;
        }
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

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = this.state
                    });
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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                    }
                }

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = this.state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });

            }else
            {
                //player not carrying anything
                this.GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = this.state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
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
