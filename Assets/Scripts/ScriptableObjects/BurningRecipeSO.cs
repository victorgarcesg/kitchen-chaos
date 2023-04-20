using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipeSO", menuName = "ScriptableObjects/BurningRecipeSO", order = 3)]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int burningTimerMax;
}
