using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interact!");
        Transform prefab = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        prefab.localPosition = Vector3.zero;
        Debug.Log(kitchenObjectSO.objectName);
    }
}
