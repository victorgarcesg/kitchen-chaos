using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved; 

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private float platesSpawnTimer;
    private readonly float platesSpawnTimerMax = 4f;
    private int platesSpawnedAmount;
    private readonly int platesSpawnedAmountMax = 4;

    private void Update()
    {
        platesSpawnTimer += Time.deltaTime;
        if (platesSpawnTimer >= platesSpawnTimerMax)
        {
            platesSpawnTimer = 0f;
            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
