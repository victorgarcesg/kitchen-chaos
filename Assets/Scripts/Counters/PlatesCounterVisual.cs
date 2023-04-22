using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualPrefabGameObjects;

    private void Awake()
    {
        plateVisualPrefabGameObjects = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        var lastPlateGameObject = plateVisualPrefabGameObjects[^1];
        plateVisualPrefabGameObjects.Remove(lastPlateGameObject);
        Destroy(lastPlateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform prefab = Instantiate(plateVisualPrefab, counterTopPoint);

        var plateOffsetY = .1f;
        prefab.localPosition = new Vector3(0f, plateOffsetY * plateVisualPrefabGameObjects.Count, 0f);        
        plateVisualPrefabGameObjects.Add(prefab.gameObject);
    }
}
