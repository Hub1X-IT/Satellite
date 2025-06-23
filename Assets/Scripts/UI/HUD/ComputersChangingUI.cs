using UnityEngine;
using UnityEngine.UI;

public class ComputersChangingUI : MonoBehaviour
{
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

    private void Awake()
    {
        changeComputerLeftButton.onClick.AddListener(changeComputerLeftGameEvent.TryRaiseEvent);
        changeComputerRightButton.onClick.AddListener(changeComputerRightGameEvent.TryRaiseEvent);

        foreach (var gameEvent in computerViewEnabledGameEvents)
        {
            gameEvent.EventRaised += EnableButtons;
        }
        foreach (var gameEvent in computerViewDisabledGameEvents)
        {
            gameEvent.EventRaised += DisableButtons;
        }

        DisableButtons();
    }

    private void EnableButtons(Computer computer)
    {
        if (computer.ComputerOnLeft != null)
        {
            changeComputerLeftButton.gameObject.SetActive(true);
        }
        if (computer.ComputerOnRight != null)
        {
            changeComputerRightButton.gameObject.SetActive(true);
        }
    }

    private void DisableButtons()
    {
        changeComputerLeftButton.gameObject.SetActive(false);
        changeComputerRightButton.gameObject.SetActive(false);
    }
}
