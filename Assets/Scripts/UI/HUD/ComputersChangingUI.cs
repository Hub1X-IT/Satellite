using UnityEngine;
using UnityEngine.UI;

public class ComputersChangingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject computerExitHint;

    [SerializeField]
    private Button changeComputerLeftButton;

    [SerializeField]
    private Button changeComputerRightButton;

    [SerializeField]
    private GameEventSO changeComputerLeftGameEvent;

    [SerializeField]
    private GameEventSO changeComputerRightGameEvent;

    [SerializeField]
    private GameEventComputerSO[] computerViewEnabledGameEvents;

    [SerializeField]
    private GameEventSO[] computerViewDisabledGameEvents;

    // May be temporary
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;


    private void Awake()
    {
        changeComputerLeftButton.onClick.AddListener(changeComputerLeftGameEvent.TryRaiseEvent);
        changeComputerRightButton.onClick.AddListener(changeComputerRightGameEvent.TryRaiseEvent);

        foreach (var gameEvent in computerViewEnabledGameEvents)
        {
            gameEvent.EventRaised += Enable;
        }
        foreach (var gameEvent in computerViewDisabledGameEvents)
        {
            gameEvent.EventRaised += Disable;
        }

        serverViewEnabledGameEvent.EventRaised += (enabled) =>
        {
            computerExitHint.SetActive(enabled);
        };

        Disable();
    }

    private void Enable(Computer computer)
    {
        computerExitHint.SetActive(true);
        if (computer.ComputerOnLeft != null)
        {
            changeComputerLeftButton.gameObject.SetActive(true);
        }
        if (computer.ComputerOnRight != null)
        {
            changeComputerRightButton.gameObject.SetActive(true);
        }
    }

    private void Disable()
    {
        computerExitHint.SetActive(false);
        changeComputerLeftButton.gameObject.SetActive(false);
        changeComputerRightButton.gameObject.SetActive(false);
    }
}
