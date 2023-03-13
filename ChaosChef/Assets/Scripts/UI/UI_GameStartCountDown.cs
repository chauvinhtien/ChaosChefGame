using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_GameStartCountDown : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI countDownText;

   private void Start()
   {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
   }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }else
        {
            Hide();
        }
    }

    private void Update() {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString();
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
