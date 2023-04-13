using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                // Player has kitchen object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            // Player has object AND the object has cutting recipe
            if (player.HasKitchenObject() && HasCuttingRecipeSO(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // No one has kitchen object
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        KitchenObjectSO kitchenObjectSO = GetKitchenObject()?.GetKitchenObjectSO();
        if (HasKitchenObject() && HasCuttingRecipeSO(kitchenObjectSO))
        {
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(GetOutputForInput(kitchenObjectSO), this);
        }
    }

    private bool HasCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSO)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipesSO)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }
}
