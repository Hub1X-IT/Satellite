using System;
using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    public event Action<bool> ComputerViewEnabled;

    private Computer computer;

    [SerializeField]
    private GameEventComputerSO computerViewEnabledGameEvent;

    [SerializeField]
    private GameEventSO computerViewDisabledGameEvent;

    [SerializeField]
    private ComputerUICursorController computerCursor;

    [SerializeField]
    private GameObject computerTurnedOffScreen;

    private CanvasGroup canvasGroup;

    private ComputerUIDynamicInputField currentSelectedInputField;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        computerViewEnabledGameEvent.EventRaised += (callerComputer) =>
        {
            // Enter computer view.
            computer = callerComputer;
            SetComputerViewEnalbed(true);
            ComputerViewEnabled?.Invoke(true);
        };

        computerViewDisabledGameEvent.EventRaised += () =>
        {
            // Exit computer view.
            computer = null;
            SetComputerViewEnalbed(false);
            ComputerViewEnabled?.Invoke(false);
        };

        DetectionManager.DetectionOccured += () =>
        {
            computerTurnedOffScreen.SetActive(true);
        };

        ServerConnectionManager.ServerConnectionEnabled += (enabled) =>
        {
            computerTurnedOffScreen.SetActive(!enabled);
        };

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            if (!inputField.readOnly)
            {
                inputField.onSelect.AddListener((_) => SetCanExitComputerViewFalse(null));
                inputField.onDeselect.AddListener((_) => SetCanExitComputerViewTrue());
            }
        }

        SetComputerViewEnalbed(false);
        computerTurnedOffScreen.SetActive(true);
    }

    private void SetComputerViewEnalbed(bool enabled)
    {
        canvasGroup.blocksRaycasts = enabled;
        computerCursor.SetEnabled(enabled);
    }

    public void AddInputField(ComputerUIDynamicInputField inputField)
    {
        inputField.InputFieldSelected += SetCanExitComputerViewFalse;
        inputField.InputFieldDeselected += SetCanExitComputerViewTrue;
    }

    public void RemoveInputField(ComputerUIDynamicInputField inputField)
    {
        inputField.InputFieldSelected -= SetCanExitComputerViewFalse;
        inputField.InputFieldDeselected -= SetCanExitComputerViewTrue;
        if (currentSelectedInputField == inputField)
        {
            SetCanExitComputerViewTrue();
        }
    }

    private void SetCanExitComputerViewFalse(ComputerUIDynamicInputField inputField)
    {
        if (computer != null)
        {
            computer.CanExitComputerView = false;
            currentSelectedInputField = inputField;
        }
    }

    private void SetCanExitComputerViewTrue()
    {
        if (computer != null)
        {
            computer.CanExitComputerView = true;
            currentSelectedInputField = null;
        }
    }
}