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
    private GameEventComputerSO changeToComputerGameEvent;

    [SerializeField]
    private GameEventComputerSO[] computerViewEnabledGameEvents;
    [SerializeField]
    private GameEventSO[] computerViewDisabledGameEvents;

    // May be temporary
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    private Computer currentComputer;
    private Computer currentComputerOnLeft;
    private Computer currentComputerOnRight;


    private void Awake()
    {
        changeComputerLeftButton.onClick.AddListener(() => TryChangeToComputer(currentComputerOnLeft));
        changeComputerRightButton.onClick.AddListener(() => TryChangeToComputer(currentComputerOnRight));

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
        currentComputer = computer;
        RefreshComputers();
        RefreshComputerChangeButtons();
    }

    // Can be made public if needed
    private void RefreshComputers()
    {
        Debug.Log(currentComputer);
        
        Computer computerOnLeft = currentComputer.ComputerOnLeft;
        while (true)
        {
            Debug.Log(computerOnLeft);
            if (computerOnLeft == null)
            {
                currentComputerOnLeft = null;
                break;
            }
            if (computerOnLeft.IsComputerEnabled)
            {
                currentComputerOnLeft = computerOnLeft;
                break;
            }
            else
            {
                computerOnLeft = computerOnLeft.ComputerOnLeft;
            }
        }

        Computer computerOnRight = currentComputer.ComputerOnRight;
        while (true)
        {
            Debug.Log(computerOnRight);
            if (computerOnRight == null)
            {
                currentComputerOnRight = null;
                break;
            }
            if (computerOnRight.IsComputerEnabled)
            {
                currentComputerOnRight = computerOnRight;
                break;
            }
            else
            {
                computerOnRight = computerOnRight.ComputerOnRight;
            }
        }
    }

    // Can be made public if needed
    private void RefreshComputerChangeButtons()
    {
        if (currentComputerOnLeft != null && currentComputerOnLeft.IsComputerEnabled)
        {
            changeComputerLeftButton.gameObject.SetActive(true);
        }
        if (currentComputerOnRight != null && currentComputerOnRight.IsComputerEnabled)
        {
            changeComputerRightButton.gameObject.SetActive(true);
        }
    }

    private void Disable()
    {
        computerExitHint.SetActive(false);
        changeComputerLeftButton.gameObject.SetActive(false);
        changeComputerRightButton.gameObject.SetActive(false);

        currentComputer = null;
    }

    private void TryChangeToComputer(Computer computer)
    {
        if (computer == null)
        {
            Debug.LogWarning("Button enabled but no computer on right/left!");
            return;
        }
        changeToComputerGameEvent.RaiseEvent(computer);
    }
}
