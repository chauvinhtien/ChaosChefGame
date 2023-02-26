using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [Header("Main Counter")]
    [SerializeField] private BaseCounter baseCounter;

    [Header("Visual Counter")]
    [SerializeField] private GameObject[] visualGameObjects;
    private void Start() {
        PlayerController.Instance.OnSelectedCounterChange += Instance_OnSelectedCounterChange;
    }

    private void Instance_OnSelectedCounterChange(object sender, PlayerController.OnSelectedCounterChangeEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject gameObject in visualGameObjects)
        {
            gameObject.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject gameObject in visualGameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
