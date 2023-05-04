using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private AvailableRecipeSO availableRecipeSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnTimer;
    private float spawnTimerMax = 4f;
    private int waitingListMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (!(spawnTimer >= spawnTimerMax)) return;
        spawnTimer = 0f;

        if (waitingRecipeSOList.Count == waitingListMax) return;

        var waitingRecipeSO = availableRecipeSO.availableRecipeSOList[Random.Range(0, availableRecipeSO.availableRecipeSOList.Count)];
        waitingRecipeSOList.Add(waitingRecipeSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> playerKitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            var waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectsSO.Count != playerKitchenObjectSOList.Count) continue;
            
            // Has same number of ingredients
            bool plateContentMatchesRecipe = waitingRecipeSO.kitchenObjectsSO.All(recipeKitchenObjectSO => playerKitchenObjectSOList.Any(playerKitchenObjectSO => recipeKitchenObjectSO == playerKitchenObjectSO));
            if (plateContentMatchesRecipe)
            {
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                
                Debug.Log("Player delivered correct recipe");
                return;
            }
        }
        
        // Player did not deliver correct recipe
        Debug.Log("Player did not deliver correct recipe");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
