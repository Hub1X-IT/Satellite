using System;
using System.Collections.Generic;
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

    private CanvasGroup canvasGroup;

    HashSet<TMP_InputField> inputFieldSet;

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

        inputFieldSet = new();

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            if (!inputField.readOnly)
            {
                AddInputField(inputField);
            }
        }

        SetComputerViewEnalbed(false);
        computerCursor.SetEnabled(false);
    }

    private void SetComputerViewEnalbed(bool enabled)
    {
        canvasGroup.blocksRaycasts = enabled;
        computerCursor.SetEnabled(enabled);
    }

    public void AddInputField(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(SetCanExitComputerViewFalse);
        inputField.onDeselect.AddListener(SetCanExitComputerViewTrue);
        inputFieldSet.Add(inputField);
    }

    public void RemoveInputField(TMP_InputField inputField)
    {
        inputField.onSelect.RemoveListener(SetCanExitComputerViewFalse);
        inputField.onDeselect.RemoveListener(SetCanExitComputerViewTrue);
        inputFieldSet.Remove(inputField);
    }

    private void SetCanExitComputerViewFalse(string _)
    {
        if (computer != null)
        {
            computer.CanExitComputerView = false;
        }
    }

    private void SetCanExitComputerViewTrue(string _)
    {
        if (computer != null)
        {
            computer.CanExitComputerView = true;
        }
    }
}