using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    /*
        Attaching this script to MonitorUI object might be a temporary solution -
        - it is better to move it to MonitorCanvas
    */

    // [SerializeField]
    private Monitor monitor;

    private TMP_InputField[] inputFields;

    private ScreenUICursorController monitorCursorController;

    [SerializeField]
    private Button testButton;

    private void Awake()
    {
        monitorCursorController = GetComponentInChildren<ScreenUICursorController>();

        // Should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();

        inputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (var inputField in inputFields)
        {
            inputField.onSelect.AddListener((_) => monitor.CanExitMonitorView = false);
            inputField.onDeselect.AddListener((_) => monitor.CanExitMonitorView = true);
        }

        monitor.MonitorViewSetActive += (enabled) => monitorCursorController.enabled = enabled;

        testButton.onClick.AddListener(() => Debug.Log("TestButton: onClick"));

        monitorCursorController.enabled = false;
    }
}