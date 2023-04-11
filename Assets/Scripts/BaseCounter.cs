using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public abstract void Interact(Player player);

    public Transform GetKitchenObjectFollowPoint() => counterTopPoint;

    public void SetKitchenObject(KitchenObject kitchenObject) => this.kitchenObject = kitchenObject;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;

    public KitchenObject GetKitchenObject() => kitchenObject;
}
