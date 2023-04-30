using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvailableRecipeSO", menuName = "ScriptableObjects/AvailableRecipeSO", order = 5)]
public class AvailableRecipeSO : ScriptableObject
{
    public List<RecipeSO> availableRecipeSOList;
}
