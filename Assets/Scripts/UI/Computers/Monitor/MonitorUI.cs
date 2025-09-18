using UnityEngine;

public class MonitorUI : MonoBehaviour
{
    [SerializeField]
    private FileExplorerUI fileExplorer;

    [SerializeField]
    private MonitorAppsManagerUI appsManager;

    [SerializeField]
    private GameObject computerTurnedOffScreen;

    public FileExplorerUI FileExplorer => fileExplorer;

    private void Awake()
    {
        fileExplorer.CurrentMonitorAppsManager = appsManager;


        DetectionManager.DetectionOccured += () =>
        {
            SetMonitorEnabled(false);
        };

        ServerConnectionManager.ServerConnectionEnabled += SetMonitorEnabled;
        DetectionManager.ServerPowerEnabled += SetMonitorEnabled;

        computerTurnedOffScreen.SetActive(true);
    }

    private void SetMonitorEnabled(bool enabled)
    {
        computerTurnedOffScreen.SetActive(!enabled);
    }
}