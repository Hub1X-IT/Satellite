using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool IsPlayerMoving;

    private CharacterController characterController;

    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed = 5f;

    private const float Gravity = -9.81f;

    [SerializeField]
    private float gravityMultiplier = 0.8f;

    private float verticalVelocity;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        IsPlayerMoving = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePaused)
        {
            HandleGravity();
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.MovementVectorNormalized;
        Vector3 movementInput = new(inputVector.x, 0f, inputVector.y);

        moveDirection = transform.right * movementInput.x + transform.forward * movementInput.z;

        IsPlayerMoving = inputVector != Vector2.zero;

        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        IsPlayerMoving = false;
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -0.1f;
        }
        else
        {
            verticalVelocity += Gravity * gravityMultiplier * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;
    }

    public void WarpPosition(Vector3 newPosition)
    {
        characterController.enabled = false;
        transform.position = newPosition;
        characterController.enabled = true;
    }
}