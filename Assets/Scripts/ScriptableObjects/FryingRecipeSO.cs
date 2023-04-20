using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "ScriptableObjects/FryingRecipeSO", order = 2)]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int fryingTimerMax;
}
