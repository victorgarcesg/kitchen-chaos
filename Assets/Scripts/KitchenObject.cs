using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    public ClearCounter GetClearCounter() => this.clearCounter;

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetClearCounterFollowPoint();
        transform.localPosition = Vector3.zero;
    }
}
