using UnityEngine;

public class ServerUI : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    private ScreenUI screenUI;

    private void Awake()
    {
        screenUI = GetComponent<ScreenUI>();

        serverViewEnabledGameEvent.EventRaised += screenUI.SetScreenViewEnalbed;

        screenUI.SetScreenViewEnalbed(false);
    }
}