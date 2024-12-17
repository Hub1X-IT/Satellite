using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileDoorAppShortcutSO")]
public class FileDoorAppShortcutSO : FileSO
{
    public MonitorAppsManagerUI.ApplicationType TriggeredApplicationType { get; }
        = MonitorAppsManagerUI.ApplicationType.DoorApp;
}
