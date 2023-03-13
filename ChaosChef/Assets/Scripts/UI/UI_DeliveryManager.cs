using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DeliveryManager : MonoBehaviour
{
    [SerializeField] private Transform containter;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start(){
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();

    }

    private void UpdateVisual()
    {
        
        foreach (Transform child in containter)
        {
            if(child == recipeTemplate)
            {
                continue;
            }else
            {
                Destroy(child.gameObject);
            }
        }
        foreach ( SO_Recipe recipeSO in  DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Debug.Log("Spawned");
            Transform recipeTransform = Instantiate(recipeTemplate, containter);

            UI_WaittingRecipeSingle waittingRecipeSingle =  recipeTransform.gameObject.GetComponent<UI_WaittingRecipeSingle>();
            
            waittingRecipeSingle.UpdateTemplateVisual(recipeSO);

            recipeTransform.gameObject.SetActive(true);
        }
        
    }
}
