using UnityEngine;

public class GuidebookUI : MonoBehaviour
{
    private ScreenUI screenUI;

    [SerializeField]
    private GameEventBoolSO guidebookViewToggledGameEvent;

    private void Awake()
    {
        screenUI = GetComponent<ScreenUI>();

        guidebookViewToggledGameEvent.EventRaised += (enabled) =>
        {
            screenUI.SetScreenViewEnalbed(enabled);
        };
    }

    private void Start()
    {
        screenUI.SetScreenViewEnalbed(false);
    }
}
