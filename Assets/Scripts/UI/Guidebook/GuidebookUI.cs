using UnityEngine;

public class GuidebookUI : MonoBehaviour
{
    private ScreenUI screenUI;

    [SerializeField]
    private GameEventComputerSO guidebookViewEnabledGameEvent;

    [SerializeField]
    private GameEventSO guidebookViewDisabledGameEvent;

    private void Awake()
    {
        screenUI = GetComponent<ScreenUI>();

        guidebookViewEnabledGameEvent.EventRaised += (_) =>
        {
            screenUI.SetScreenViewEnalbed(true);
        };

        guidebookViewDisabledGameEvent.EventRaised += () =>
        {
            screenUI.SetScreenViewEnalbed(false);
        };
    }

    private void Start()
    {
        screenUI.SetScreenViewEnalbed(false);
    }
}
