using TMPro;
using UnityEngine;

public class MonitorUI : MonoBehaviour {

    [SerializeField] private Monitor monitor;

    [SerializeField] private TMP_InputField[] inputFields;

    private void Start() {
        // monitor = FindAnyObjectByType<Monitor>();
        inputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField inputField in inputFields) {
            inputField.onSelect.AddListener((_) => { monitor.CanExitMonitorView = false; });
            inputField.onDeselect.AddListener((_) => { monitor.CanExitMonitorView = true; });
        }
    }
}