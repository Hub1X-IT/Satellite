using System.Collections;
using UnityEngine;

public class ComputerUICursorController : MonoBehaviour
{
    private RectTransform rectTransform;

    private readonly Vector2 defaultPosition = new(50f, -50f);

    // Temporary solution - should be dynamic, dependent on current resolution.
    private Vector2 positionAddition;

    [SerializeField]
    private Vector2 canvasSize = new(1920f, 1080f);

    private Vector2 positionMultiplier;

    private bool shouldUpdatePosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = defaultPosition;

        GameManager.GamePausedUnpaused += (_) =>
        {
            SetPositionMultiplier();
        };

        SetPositionMultiplier();
    }

    private void Update()
    {
        if (shouldUpdatePosition)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        Vector2 newPosition = GameInput.CursorPosition * positionMultiplier + positionAddition;
        rectTransform.anchoredPosition = newPosition;
    }

    public void SetCursorEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (enabled)
        {
            Vector2 newPosition = (rectTransform.anchoredPosition - positionAddition) / positionMultiplier;
            StartCoroutine(SetMousePositionOnNextFrame(newPosition));
        }
    }

    private IEnumerator SetMousePositionOnNextFrame(Vector2 position)
    {
        shouldUpdatePosition = false;
        yield return null;
        GameInput.SetMousePosition(position);
        shouldUpdatePosition = true;
    }

    private void SetPositionMultiplier()
    {
        Resolution currentResolution = Screen.currentResolution;
        Vector2 currentScreenSize = new(currentResolution.width, currentResolution.height);
        Debug.Log(currentScreenSize);
        Debug.Log(canvasSize);
        positionMultiplier = canvasSize / currentScreenSize;
        positionAddition = new(0f, -canvasSize.y);
        Debug.Log(positionMultiplier);
    }
}
