using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    // [SerializeField]
    private Monitor monitor;

    private TMP_InputField[] inputFields;

    private ScreenUICursorController monitorCursorController;

    private MonitorUIStartMenu startMenu;

    [SerializeField]
    private Button testButton;

    [SerializeField]
    private Button startMenuButton;

    private void Awake()
    {
        monitorCursorController = GetComponentInChildren<ScreenUICursorController>();

        startMenu = GetComponentInChildren<MonitorUIStartMenu>(true);

        // Should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();

        inputFields = GetComponentsInChildren<TMP_InputField>(includeInactive: true);
        foreach (var inputField in inputFields)
        {
            inputField.onSelect.AddListener((_) => monitor.CanExitMonitorView = false);
            inputField.onDeselect.AddListener((_) => monitor.CanExitMonitorView = true);
        }

        monitor.MonitorViewSetActive += (enabled) => monitorCursorController.enabled = enabled;

        testButton.onClick.AddListener(() => Debug.Log($"{testButton.name}: {nameof(testButton.onClick)}"));

        monitorCursorController.enabled = false;
    }
}