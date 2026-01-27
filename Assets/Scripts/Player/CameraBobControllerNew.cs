using UnityEngine;

public class CameraBobControllerNew : MonoBehaviour
{
    [SerializeField]
    private Transform cameraFollowTransform;
    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField] 
    private float bobSpeed = 10f;
    [SerializeField] 
    private float bobAmount = 0.05f;
    [SerializeField] 
    private float swayAmount = 0.03f;
    [SerializeField] 
    private float smooth = 8f;

    private float timer;
    private float bobWeight; // 0 = no bob, 1 = full bob
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = cameraFollowTransform.localPosition;
    }

    private void Update()
    {
        bool isMoving = playerMovementController.IsPlayerMoving; 
        // Smoothly blend bob in/out
        bobWeight = Mathf.Lerp(
            bobWeight,
            isMoving ? 1f : 0f,
            Time.deltaTime * smooth
        );

        timer += Time.deltaTime * bobSpeed;

        float bobOffset = Mathf.Sin(timer) * bobAmount * bobWeight;
        float swayOffset = Mathf.Cos(timer * 0.5f) * swayAmount * bobWeight;

        Vector3 targetPos = startPosition + new Vector3(swayOffset, bobOffset, 0f);

        cameraFollowTransform.localPosition = Vector3.Lerp(
            cameraFollowTransform.localPosition,
            targetPos,
            Time.deltaTime * smooth
        );
    }
}
