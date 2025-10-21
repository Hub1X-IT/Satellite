using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputersChangingUI : MonoBehaviour
{
    public static event Action ComputerExitTriggered;

    [SerializeField]
    private Button changeComputerLeftButton;
    [SerializeField]
    private Button changeComputerRightButton;

    [SerializeField]
    private TMP_Text leftComputerText;
    [SerializeField]
    private TMP_Text rightComputerText;

    [SerializeField]
    private GameEventComputerSO changeToComputerGameEvent;

    [SerializeField]
    private GameEventComputerSO[] computerViewEnabledGameEvents;
    [SerializeField]
    private GameEventSO[] computerViewDisabledGameEvents;

    // May be temporary
    [SerializeField]
    private GameEventBoolSO serverViewEnabledGameEvent;

    [SerializeField]
    private Button exitComputerViewButton;

    private Computer currentComputer;
    private Computer currentComputerOnLeft;
    private Computer currentComputerOnRight;


    private void Awake()
    {
        changeComputerLeftButton.onClick.AddListener(() => TryChangeToComputer(currentComputerOnLeft));
        changeComputerRightButton.onClick.AddListener(() => TryChangeToComputer(currentComputerOnRight));

        exitComputerViewButton.onClick.AddListener(() => ComputerExitTriggered?.Invoke());

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
            exitComputerViewButton.gameObject.SetActive(enabled);
        };

        Disable();
    }

    private void Oestroy()
    {
        ComputerExitTriggered = null;
    }

    private void Enable(Computer computer)
    {
        exitComputerViewButton.gameObject.SetActive(true);
        currentComputer = computer;
        RefreshComputers();
        RefreshComputerChangeButtons();
    }

    // Can be made public if needed
    private void RefreshComputers()
    {
        Computer computerOnLeft = currentComputer.ComputerOnLeft;
        while (true)
        {
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
            leftComputerText.text = currentComputerOnLeft.ChangeToComputerText;
        }
        if (currentComputerOnRight != null && currentComputerOnRight.IsComputerEnabled)
        {
            changeComputerRightButton.gameObject.SetActive(true);
            rightComputerText.text = currentComputerOnRight.ChangeToComputerText;
        }
    }

    private void Disable()
    {
        changeComputerLeftButton.gameObject.SetActive(false);
        changeComputerRightButton.gameObject.SetActive(false);
        exitComputerViewButton.gameObject.SetActive(false);

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
