using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public event Action<bool> StartedMoving;

    private CharacterController characterController;

    private Vector3 moveDirection;

    [SerializeField]
    private float moveSpeed = 5f;

    private const float Gravity = -9.81f;

    [SerializeField]
    private float gravityMultiplier = 0.8f;

    private float verticalVelocity;

    private bool wasMoving;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        wasMoving = false;
    }

    private void Update()
    {
        if (!GameManager.IsGamePaused)
        {
            HandleGravity();
            HandleMovement();
        }
    }

    private void OnDestroy()
    {
        StartedMoving = null;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.MovementVectorNormalized;
        Vector3 movementInput = new(inputVector.x, 0f, inputVector.y);

        moveDirection = transform.right * movementInput.x + transform.forward * movementInput.z;

        if (!wasMoving && inputVector != Vector2.zero)
        {
            StartedMoving?.Invoke(true);
            wasMoving = true;
        }
        else if (wasMoving && inputVector == Vector2.zero)
        {
            StartedMoving?.Invoke(false);
            wasMoving = false;
        }

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