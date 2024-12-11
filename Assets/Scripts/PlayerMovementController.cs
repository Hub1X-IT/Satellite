using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed = 5f;

    private const float GRAVITY = -9.81f;

    [SerializeField]
    private float gravityMultiplier = 0.8f;

    private float verticalVelocity;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!GameManager.IsGamePaused)
        {
            HandleGravity();
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.MovementVectorNormalized;
        Vector3 movementInput = new(inputVector.x, 0f, inputVector.y);

        moveDirection = transform.right * movementInput.x + transform.forward * movementInput.z;
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -0.1f;
        }
        else
        {
            verticalVelocity += GRAVITY * gravityMultiplier * Time.deltaTime;
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