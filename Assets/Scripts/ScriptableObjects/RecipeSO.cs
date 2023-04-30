using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "ScriptableObjects/RecipeSO", order = 4)]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectsSO;
    public string recipeName;
}
