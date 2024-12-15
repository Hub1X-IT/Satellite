using UnityEngine;

public class ServerUI : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    [SerializeField]
    private ComputerUICursorController serverCursor;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        serverViewEnabledGameEvent.EventRaised += (enabled) => SetServerViewEnalbed(enabled);

        SetServerViewEnalbed(false);
    }

    private void SetServerViewEnalbed(bool enabled)
    {
        canvasGroup.blocksRaycasts = enabled;
        serverCursor.SetEnabled(enabled);
    }
}