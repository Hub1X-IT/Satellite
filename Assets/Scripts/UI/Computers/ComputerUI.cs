using System;
using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    public event Action<bool> ComputerViewEnabled;

    private Computer computer;

    private ScreenUI screenUI;

    [SerializeField]
    private GameEventComputerSO computerViewEnabledGameEvent;

    [SerializeField]
    private GameEventSO computerViewDisabledGameEvent;

    [SerializeField]
    private GameObject computerTurnedOffScreen;

    private ComputerUIDynamicInputField currentSelectedInputField;

    private void Awake()
    {
        screenUI = GetComponent<ScreenUI>();

        computerViewEnabledGameEvent.EventRaised += (callerComputer) =>
        {
            // Enter computer view.
            computer = callerComputer;
            screenUI.SetScreenViewEnalbed(true);
            ComputerViewEnabled?.Invoke(true);
        };

        computerViewDisabledGameEvent.EventRaised += () =>
        {
            // Exit computer view.
            computer = null;
            screenUI.SetScreenViewEnalbed(false);
            ComputerViewEnabled?.Invoke(false);
        };

        DetectionManager.DetectionOccured += () =>
        {
            SetComputerEnabled(false);
        };

        ServerConnectionManager.ServerConnectionEnabled += SetComputerEnabled;

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            if (!inputField.readOnly)
            {
                inputField.onSelect.AddListener((_) => SetCanExitComputerViewFalse(null));
                inputField.onDeselect.AddListener((_) => SetCanExitComputerViewTrue());
            }
        }

        screenUI.SetScreenViewEnalbed(false);
        computerTurnedOffScreen.SetActive(true);
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

    private void SetComputerEnabled(bool enabled)
    {
        computerTurnedOffScreen.SetActive(!enabled);
        screenUI.RenderScreen();
    }
}