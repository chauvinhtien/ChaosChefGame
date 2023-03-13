using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private float plateHeight;

    private List<GameObject> plateVisualGameObjectList;
    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpwaned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateSpwaned(object sender, EventArgs e)
    {
       
        Transform plateCounterVisual =  Instantiate(plateVisualPrefab, counterTopPoint);
        plateCounterVisual.localPosition = new Vector3(0, plateHeight * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateCounterVisual.gameObject);
    }
    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count -1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
