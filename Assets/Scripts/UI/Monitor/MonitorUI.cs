using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    // [SerializeField]
    private Monitor monitor;

    private TMP_InputField[] inputFields;

    [SerializeField]
    private ScreenUICursorController monitorCursor;

    [SerializeField]
    private Button testButton;

    private void Awake()
    {
        // Should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();

        inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            inputField.onSelect.AddListener((_) => monitor.CanExitMonitorView = false);
            inputField.onDeselect.AddListener((_) => monitor.CanExitMonitorView = true);
        }

        monitor.MonitorViewSetActive += (enabled) => monitorCursor.enabled = enabled;

        testButton.onClick.AddListener(() => Debug.Log($"{testButton.name}: {nameof(testButton.onClick)}"));

        monitorCursor.enabled = false;
    }
}