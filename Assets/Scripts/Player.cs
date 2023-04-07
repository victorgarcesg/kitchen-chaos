using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float moveRotation = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDir != Vector3.zero;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * moveRotation);
    }

    public bool IsWalking() => isWalking;
}
