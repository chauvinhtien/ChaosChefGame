using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Progressbar : MonoBehaviour
{
    [SerializeField] GameObject HasProgressObject;
    private IHasProgress hasProgress;
    [SerializeField] private Image barImage;
    

    private void Start() {
        hasProgress = HasProgressObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError(gameObject + "does not has IHasProgress");
        } 
        hasProgress.OnProgressChanged += HasProgress_OnProgessChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgessChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
