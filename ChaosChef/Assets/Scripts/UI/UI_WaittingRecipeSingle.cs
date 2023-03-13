using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_WaittingRecipeSingle : MonoBehaviour
{
    [SerializeField] private TMP_Text waittingRecipeText;

    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform kitchenObjectIconUI;

    private void Awake() {
        kitchenObjectIconUI.gameObject.SetActive(false);
    }
    public void UpdateTemplateVisual( SO_Recipe recipeSO)
    {
        waittingRecipeText.text = recipeSO.recepieName;
        foreach (Transform child in iconContainer)
        {
            if(child == kitchenObjectIconUI)
            {
                continue;
            }else
            {
                Destroy(child.gameObject);
            }
        }
        foreach (SO_KitchenObject kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform kitchenObjectIconTransform = Instantiate(kitchenObjectIconUI, iconContainer);
            
            kitchenObjectIconTransform.gameObject.GetComponent<Image>().sprite = kitchenObjectSO.sprite;

            kitchenObjectIconTransform.gameObject.SetActive(true);
        }
    }
}
