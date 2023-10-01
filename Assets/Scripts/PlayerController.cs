using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {


    [SerializeField] private Transform cameraFollowObject;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity; // Temporary solution; remember to make it possible to change sensitivity in settings!\
    [SerializeField] private float gravityMultiplier;


    private CharacterController characterController;


    private Vector3 movementInput;
    private Vector3 moveDirection;


    private Vector2 rotationInput;


    private const float gravity = -9.81f;
    private float verticalVelocity;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        HandleGravity();
        HandleRotation();
        HandleMovement();
    }


    public void OnMove(InputAction.CallbackContext callbackContext) {
        Vector2 input = callbackContext.ReadValue<Vector2>();
        movementInput = new Vector3(input.x, 0f, input.y);
    }

    public void OnRotate(InputAction.CallbackContext callbackContext) {
        rotationInput = callbackContext.ReadValue<Vector2>();
    }

    public void OnInteract() {
        Debug.Log("Player: OnInteract");
    }


    private void HandleMovement() {
        moveDirection = transform.right * movementInput.x + transform.forward * movementInput.z;
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation() {
        // Handle Y axis rotation - rotating the player
        Vector3 playerRotation = new Vector3(0f, rotationInput.x, 0f);
        transform.Rotate(playerRotation * mouseSensitivity * Time.deltaTime);

        // Handle X axis rotation - rotating only the camera
        Vector3 cameraRotation = cameraFollowObject.rotation.eulerAngles;
        
        if (cameraRotation.x > 180f) {
            cameraRotation.x -= 360f;
        }
        {   // eulerAngles always maps rotation to a positive value so if it more than 180 degrees, we have to make it negative so that we can clamp it
            // For example: when rotation equals -20, we get 340 which is greater than 90 so it would be clamped to 90 degrees, although it should have been left as it is
        }
        cameraRotation.x += -rotationInput.y * mouseSensitivity * Time.deltaTime;

        // Clamp camera X rotation
        float minXRotation = -90f;
        float maxXRotation = 90f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, minXRotation, maxXRotation);

        cameraFollowObject.rotation = Quaternion.Euler(cameraRotation);
    }

    private void HandleGravity() {
        if (characterController.isGrounded && verticalVelocity < 0f) {
            verticalVelocity = -1f;
        }
        else {
            verticalVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;
    }

}