using UnityEngine;

public class ScreenUI : MonoBehaviour
{
    [SerializeField]
    private ComputerUICursorController screenCursor;

    [SerializeField]
    private Camera screenUICamera;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        SetScreenViewEnalbed(false);
    }

    private void Start()
    {
        RenderScreen();
    }

    public void SetScreenViewEnalbed(bool enabled)
    {
        canvasGroup.blocksRaycasts = enabled;
        screenCursor.SetCursorEnabled(enabled);
        screenUICamera.gameObject.SetActive(enabled);
    }

    public void RenderScreen()
    {
        screenUICamera.Render();
    }
}