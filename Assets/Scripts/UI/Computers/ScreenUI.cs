using UnityEngine;

public class ScreenUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        SetScreenViewEnalbed(false);
    }

    public void SetScreenViewEnalbed(bool enabled)
    {
        canvasGroup.blocksRaycasts = enabled;
    }
}