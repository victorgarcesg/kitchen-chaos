using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float moveRotation = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        var interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance, layerMask)
            && raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            clearCounter.Interact();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;

        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = 0.7f;
        var playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Can not move towards moveDir

            // Attempt only X movement
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * moveRotation);
    }

    public bool IsWalking() => isWalking;
}
