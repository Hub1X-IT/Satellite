using UnityEngine;

public class ScreenUICursorController : MonoBehaviour
{
    private RectTransform rectTransform;

    private Vector2 currentPosition;

    private readonly Vector2 defaultPosition = new(50f, 50f);

    // So that the y position can be positive and can be counted from the top-left corner at the same time.
    // Can be removed if considered unnecessary.
    private readonly Vector2 positionMultiplier = new(1f, -1f);

    private readonly float sensitivityMultiplier = 5f;

    private IScreenUIInteractable currentUIInteractable;

    private bool shouldSelectUIInteractable;

    private readonly float minXPosition = 0f;
    private readonly float maxXPosition = 1920f;
    private readonly float minYPosition = 0f;
    private readonly float maxYPosition = 1080f;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        currentPosition = defaultPosition;
        rectTransform.anchoredPosition = currentPosition * positionMultiplier;

        shouldSelectUIInteractable = false;

        GameInput.OnLeftMouseButtonAction += GameInput_OnLeftMouseButtonAction;
        GameInput.OnRightMouseButtonAction += GameInput_OnRightMouseButtonAction;
    }

    private void Update()
    {
        if (shouldSelectUIInteractable)
        {
            currentUIInteractable?.Select();
            shouldSelectUIInteractable = false;
        }
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentUIInteractable = collision.GetComponent<IScreenUIInteractable>();
        currentUIInteractable?.Select();
        
        Debug.Log(currentUIInteractable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentUIInteractable?.Deselect();
        currentUIInteractable = null;
        shouldSelectUIInteractable = false;
    }

    private void HandleMovement()
    {
        currentPosition += GameSettingsManager.MouseSensitivity * sensitivityMultiplier * 
            GameInput.MouseMovementVector * positionMultiplier;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minXPosition, maxXPosition);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minYPosition, maxYPosition);

        rectTransform.anchoredPosition = currentPosition * positionMultiplier;
    }
    
    private void GameInput_OnLeftMouseButtonAction()
    {
        currentUIInteractable?.LeftClick();
        shouldSelectUIInteractable = true;
    }

    private void GameInput_OnRightMouseButtonAction()
    {
        currentUIInteractable?.RightClick();
        shouldSelectUIInteractable = true;
    }
}
