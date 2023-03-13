using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private SO_KitchenObject plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private int plateSpwanAmount;
    private int plateSpwanAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if(plateSpwanAmount < plateSpwanAmountMax)
            {
                plateSpwanAmount ++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        if(!player.HasKitChenObject())
        {
            //Player hand is empty
            if(plateSpwanAmount > 0)
            {
                //There is plate on counter
                plateSpwanAmount--;
                KitchenObject.SpawnKitChenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
