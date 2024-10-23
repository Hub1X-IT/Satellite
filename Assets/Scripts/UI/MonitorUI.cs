using TMPro;
using UnityEngine;

public class MonitorUI : MonoBehaviour {

    [SerializeField] private Monitor monitor;

    [SerializeField] private TMP_InputField[] inputFields;

    private void Start() {
        // monitor = FindFirstObjectByType<Monitor>();
        inputFields = GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField inputField in inputFields) {
            inputField.onSelect.AddListener((string _) => { monitor.CanExitMonitorView = false; });
            inputField.onDeselect.AddListener((string _) => { monitor.CanExitMonitorView = true; });
        }
    }
}