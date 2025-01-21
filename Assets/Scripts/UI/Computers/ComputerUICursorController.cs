using UnityEngine;

public class ComputerUICursorController : MonoBehaviour
{
    private RectTransform rectTransform;

    private const float SensitivityMultiplier = 10f;

    private readonly Vector2 defaultPosition = new(50f, -50f);

    [SerializeField]
    private Vector2 canvasSize = new(1920f, 1080f);

    private Vector2 currentPosition;

    private bool isCursorActive;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        GameInput.OnLeftClickPerformedAction += TryPerformLeftClick;

        currentPosition = defaultPosition;
        rectTransform.anchoredPosition = currentPosition - new Vector2(0f, canvasSize.y);

        isCursorActive = false;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 positionShift = GameInput.RotationVector * (GameSettingsManager.MouseSensitivity * SensitivityMultiplier);
        Vector2 newPosition = new(Mathf.Clamp(currentPosition.x + positionShift.x, 0f, canvasSize.x),
            Mathf.Clamp(currentPosition.y + positionShift.y, 0f, canvasSize.y));
        currentPosition = newPosition;
        rectTransform.anchoredPosition = currentPosition - new Vector2(0f, canvasSize.y);
    }

    private void TryPerformLeftClick()
    {
        // Checking for paused game may be a temporary fix.
        if (isCursorActive && !GameManager.IsGamePaused)
        {
            MouseClick.SimulateClick(currentPosition);
        }
    }

    public void SetCursorEnabled(bool enabled)
    {
        this.enabled = isCursorActive = enabled;
    }
}
