using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance {get; private set;}
    [SerializeField] private SO_RecipeList recipeListSO;
    private List<SO_Recipe> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private int delieveredRecipes;

    private void Awake() {
        delieveredRecipes = 0;
        Instance = this;
        waitingRecipeSOList = new List<SO_Recipe>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {

            if(waitingRecipeSOList.Count < waitingRecipeMax )
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                SO_Recipe waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSO.name);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            } 
        }
    }

    public void DeliveRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            SO_Recipe waitingRecipeSo = waitingRecipeSOList[i];
            if(waitingRecipeSo.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredient
                bool plateContentMatchesRecipe = true;
                foreach (SO_KitchenObject recipeKitchenObjectSO in waitingRecipeSo.kitchenObjectSOList)
                {   
                    //Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (SO_KitchenObject plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all the ingredients in the Plate

                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingredient matches;
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        // This Recipe ingredient was not found in the plate
                        plateContentMatchesRecipe = false;
                    }
                }
                if(plateContentMatchesRecipe)
                {
                    //Player deliever the correct recipe
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    delieveredRecipes ++;
                    return;
                }
            }
        }
        //No matches Found!
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe");
        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    public List<SO_Recipe> GetWaitingRecipeList()
    {
        return waitingRecipeSOList;
    }

    public int GetSucessfulRecipeAmount()
    {
        return delieveredRecipes;
    }
    
}
