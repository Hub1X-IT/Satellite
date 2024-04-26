using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {


    [SerializeField] private Transform cameraFollowObject;


    private CharacterController characterController;

        
    private Vector3 moveDirection;
    private float moveSpeed = 5f;


    private Vector2 rotationInput;
    public float mouseSensitivity = 25f; // Temporary solution; remember to make it possible to change sensitivity in settings!


    private const float gravity = -9.81f;
    private float gravityMultiplier = 3f;
    private float verticalVelocity;


    public Slider slider;
    public TMP_Text sensitivityPercentValue;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 25);
        slider.value = mouseSensitivity;
        SensitivityPercentValueChange();
    }

    private void Update() {
        HandleGravity();
        HandleRotation();
        HandleMovement();
        PlayerPrefs.SetFloat("currentSensitivity", mouseSensitivity);
    }

    private void HandleMovement() {        
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 movementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        
        moveDirection = transform.right * movementInput.x + transform.forward * movementInput.z;
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    
    private void HandleRotation() {
        rotationInput = GameInput.Instance.GetRotationVector();

        // Handle Y axis rotation - rotating the player
        Vector3 playerRotation = new Vector3(0f, rotationInput.x, 0f);
        transform.Rotate(playerRotation * mouseSensitivity * Time.deltaTime);

        // Handle X axis rotation - rotating only the camera
        Vector3 cameraRotation = cameraFollowObject.rotation.eulerAngles;
        
        if (cameraRotation.x > 180f) {
            cameraRotation.x -= 360f;
        }
        {   // eulerAngles always maps rotation to a positive value so if it is more than 180 degrees, we have to make it negative so that we can clamp it
            // For example: when rotation equals -20, we get 340 which is greater than 90 so it would be clamped to 90 degrees, although it should have been left as it is
        }
        cameraRotation.x += -rotationInput.y * mouseSensitivity * Time.deltaTime;

        // Clamp camera X rotation
        float minXRotation = -90f;
        float maxXRotation = 90f;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, minXRotation, maxXRotation);

        cameraFollowObject.rotation = Quaternion.Euler(cameraRotation);
    }

    public void AdjustSensitivityFromSlider()
    {
        mouseSensitivity = slider.value;
        SensitivityPercentValueChange();
    }

    private void SensitivityPercentValueChange()
    {
        sensitivityPercentValue.text = Mathf.Round(slider.value * 4).ToString();
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