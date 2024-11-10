using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    // This script doesn't work yet!

    private Monitor monitor; // temporary solution

    [SerializeField]
    private NewGameEventSO<Computer> computerViewEnabledGameEvent;

    private CanvasGroup canvasGroup;

    private List<TMP_InputField> inputFieldList;

    [SerializeField]
    private ScreenUICursorController computerCursor;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        inputFieldList = new();

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            AddInputField(inputField);
        }

        computerCursor.enabled = false;
    }


    private void AddInputField(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(SetCanExitComputerViewFalse);
        inputField.onDeselect.AddListener(SetCanExitComputerViewTrue);
        inputFieldList.Add(inputField);
    }

    private void RemoveInputField(TMP_InputField inputField)
    {
        inputField.onSelect.RemoveListener(SetCanExitComputerViewFalse);
        inputField.onDeselect.RemoveListener(SetCanExitComputerViewTrue);
        inputFieldList.Remove(inputField);
    }


    private void SetCanExitComputerViewFalse(string _) => monitor.CanExitMonitorView = false;

    private void SetCanExitComputerViewTrue(string _) => monitor.CanExitMonitorView = true;
}