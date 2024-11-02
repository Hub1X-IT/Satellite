using System.Collections;
using UnityEngine;

public class ScreenUICursorController : MonoBehaviour
{
    private RectTransform rectTransform;

    private readonly Vector2 defaultPosition = new(50f, -50f);

    // Temporary solution - should be dynamic, dependent on current resolution.
    private readonly Vector2 positionAddition = new(0f, -1080f);

    private bool shouldUpdatePosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = defaultPosition;
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
        rectTransform.anchoredPosition = GameInput.MousePosition + positionAddition;
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (enabled)
        {
            StartCoroutine(SetMousePositionOnNextFrame(rectTransform.anchoredPosition - positionAddition));
        }
    }

    private IEnumerator SetMousePositionOnNextFrame(Vector2 position)
    {
        shouldUpdatePosition = false;
        yield return null;
        shouldUpdatePosition = true;
        GameInput.SetMousePosition(position);
    }
}
