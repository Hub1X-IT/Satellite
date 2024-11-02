using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    // [SerializeField]
    private Monitor monitor;

    private List<TMP_InputField> inputFieldList;

    [SerializeField]
    private ScreenUICursorController monitorCursor;

    [SerializeField]
    private Button testButton;

    private void Awake()
    {
        // There should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();

        inputFieldList = new();

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            AddInputField(inputField);
        }

        monitor.MonitorViewSetActive += (enabled) =>
        {
            monitorCursor.SetEnabled(enabled);
        };

        testButton.onClick.AddListener(() => Debug.Log($"{testButton.name}: {nameof(testButton.onClick)}"));

        monitorCursor.enabled = false;
    }

    public void AddInputField(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(SetCanExitMonitorViewTrue);
        inputField.onDeselect.AddListener(SetCanExitMonitorViewFalse);
        inputFieldList.Add(inputField);
    }

    public void RemoveInputField(TMP_InputField inputField)
    {
        inputField.onSelect.RemoveListener(SetCanExitMonitorViewTrue);
        inputField.onDeselect.RemoveListener(SetCanExitMonitorViewFalse);
        inputFieldList.Remove(inputField);
    }

    private void SetCanExitMonitorViewTrue(string _) => monitor.CanExitMonitorView = true;

    private void SetCanExitMonitorViewFalse(string _) => monitor.CanExitMonitorView = false;
}