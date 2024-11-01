using System.Collections.Generic;
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

    private IScreenUIInteractable currentInteractable;

    /*
    private List<IScreenUIInteractable> previousInteractablesList = new();
    */

    private readonly float minXPosition = 0f;
    private readonly float maxXPosition = 1920f;
    private readonly float minYPosition = 0f;
    private readonly float maxYPosition = 1080f;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        currentPosition = defaultPosition;
        rectTransform.anchoredPosition = currentPosition * positionMultiplier;

        GameInput.OnLeftMouseButtonAction += GameInput_OnLeftMouseButtonAction;
        GameInput.OnRightMouseButtonAction += GameInput_OnRightMouseButtonAction;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentInteractable = collision.GetComponent<IScreenUIInteractable>();
        currentInteractable?.SetHighlighted(true);

        if (currentInteractable.GetType() == typeof(ScreenUIInputField))
        {
            // change the cursor appearance
        }

        Debug.Log(currentInteractable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentInteractable != null)
        {
            currentInteractable.SetHighlighted(false);
            /*
            previousInteractablesList.Add(currentInteractable);
            */
        }

        // change the cursor appearance (from text cursor)
        currentInteractable = null;
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
        if (currentInteractable != null)
        {
            currentInteractable.LeftClick();
        }
        /*
        DeselectPreviousInteractables();
        */
    }

    private void GameInput_OnRightMouseButtonAction()
    {
        if (currentInteractable != null)
        {
            currentInteractable?.RightClick();
        }
        /*
        DeselectPreviousInteractables();
        */
    }

    /*
    private void DeselectPreviousInteractables()
    {
        foreach (var interactable in previousInteractablesList)
        {
            interactable.Deselect();
        }
        previousInteractablesList.Clear();
    }
    */

    
}
