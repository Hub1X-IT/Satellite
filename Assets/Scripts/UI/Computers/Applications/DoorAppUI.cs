using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorAppUI : MonoBehaviour
{
    private MonitorAppUI monitorAppUI;

    [SerializeField]
    private GameEventSO onDoorOpenGameEvent;

    [SerializeField]
    private Button doorButton;

    [SerializeField]
    private TMP_Text doorAppTextField;

    private bool isDoorOpen;

    private const string DoorClosedText = "Door Closed";
    private const string DoorOpenText = "Door Open";

    private void InitializeDoorApp(string appName)
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(appName);

        doorButton.onClick.AddListener(() => SetDoorOpen(!isDoorOpen));

        SetDoorOpen(false);
    }

    private void SetDoorOpen(bool open)
    {
        isDoorOpen = open;
        doorAppTextField.text = open ? DoorOpenText : DoorClosedText;
        if (open && onDoorOpenGameEvent != null)
        {
            onDoorOpenGameEvent.RaiseEvent();
        }
    }
}
