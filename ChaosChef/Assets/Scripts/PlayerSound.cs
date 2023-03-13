using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerController player;
    private float footStepTimer;
    private float footStepTImerMax = 0.1f;

    [SerializeField] private float volume = 1f;


    private void Awake() {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f)
        {
            footStepTimer = footStepTImerMax;
            if(player.IsWalking)
            {
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
        }
    }
    
}
