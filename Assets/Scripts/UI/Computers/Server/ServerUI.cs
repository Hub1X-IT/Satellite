using UnityEngine;

public class ServerUI : MonoBehaviour
{
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    [SerializeField]
    private GameObject serverTurnedOffScreen;

    private ScreenUI screenUI;

    private void Awake()
    {
        screenUI = GetComponent<ScreenUI>();

        serverViewEnabledGameEvent.EventRaised += screenUI.SetScreenViewEnalbed;

        screenUI.SetScreenViewEnalbed(false);
    }

    private void Start()
    {
        PowerManager.Instance.OnPowerStateChanged += (enabled) =>
        {
            serverTurnedOffScreen.SetActive(!enabled);
        };
    }
}