using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowPoint();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
