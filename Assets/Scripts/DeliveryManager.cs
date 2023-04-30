using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
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
        Debug.Log(waitingRecipeSO);
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
            if (!plateContentMatchesRecipe)
            {
                Debug.Log("Player delivered the correct recipe");
                waitingRecipeSOList.RemoveAt(i);
                return;
            }
        }
        
        Debug.Log("Player did not deliver correct recipe");
    }
}
