using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlateIconSingle : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    public void SetKitchenObjectSO(SO_KitchenObject kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.sprite;
    }
}
