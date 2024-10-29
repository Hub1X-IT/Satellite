using TMPro;
using UnityEngine;

public class MonitorUI : MonoBehaviour
{
    private Monitor monitor;

    private TMP_InputField[] inputFields;

    private void Awake()
    {
        monitor = GetComponentInParent<Monitor>();

        inputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (var inputField in inputFields)
        {
            inputField.onSelect.AddListener((_) => monitor.CanExitMonitorView = false);
            inputField.onDeselect.AddListener((_) => monitor.CanExitMonitorView = true);
        }
    }

    public void OpenFileExplorer()
    {

    }
}