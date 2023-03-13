using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}
    [SerializeField] private SO_AudioClipRefs audioClipRefsSO;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerController.Instance.OnPicSomething += PlayerController_OnPickSomeThing;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlayMultipleSound(audioClipRefsSO.trash, trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayMultipleSound(audioClipRefsSO.objectDrop,baseCounter.transform.position);
    }

    private void PlayerController_OnPickSomeThing(object sender, EventArgs e)
    {
        PlayerController player = PlayerController.Instance;
        PlayMultipleSound(audioClipRefsSO.objectPickUp, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayMultipleSound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCouter deliveryCouter = DeliveryCouter.Instance;

        PlayMultipleSound(audioClipRefsSO.deliveryFail, deliveryCouter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCouter deliveryCouter = DeliveryCouter.Instance;
        PlayMultipleSound(audioClipRefsSO.deliverySuccess, deliveryCouter.transform.position);
    }
    private void PlayMultipleSound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {

        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume = 1f)
    {
        PlayMultipleSound(audioClipRefsSO.footStep, position, volume);
    }
}
