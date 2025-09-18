using UnityEngine;

public class MonitorUI : MonoBehaviour
{
    [SerializeField]
    private FileExplorerUI fileExplorer;

    [SerializeField]
    private MonitorAppsManagerUI appsManager;

    [SerializeField]
    private MonitorStartupScreenUI monitorStartupScreenUI;

    [SerializeField]
    private GameObject computerTurnedOffScreen;

    public FileExplorerUI FileExplorer => fileExplorer;

    private void Awake()
    {
        fileExplorer.CurrentMonitorAppsManager = appsManager;


        DetectionManager.DetectionOccured += () =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);
        };

        ServerConnectionManager.ServerConnectionEnabled += (enabled) =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            if (enabled)
            {
                monitorStartupScreenUI.StartStartupScreen(() => monitorStartupScreenUI.gameObject.SetActive(false));
            }
            else
            {
                monitorStartupScreenUI.StartStartupScreen(null);
            }
        };

        DetectionManager.ServerPowerEnabled += (enabled) =>
        {
            if (enabled)
            {
                SetMonitorEnabled(true);
                monitorStartupScreenUI.gameObject.SetActive(true);
                if (ServerConnectionManager.IsConnectionActive)
                {
                    monitorStartupScreenUI.StartStartupScreen(() => monitorStartupScreenUI.gameObject.SetActive(false));
                }
                else
                {
                    monitorStartupScreenUI.StartStartupScreen(null);
                }
            }
            else
            {
                SetMonitorEnabled(false);
                monitorStartupScreenUI.gameObject.SetActive(false);
            }
        };

        SetMonitorEnabled(true);
    }

    private void Start()
    {
        monitorStartupScreenUI.StartStartupScreen(null);
    }

    private void SetMonitorEnabled(bool enabled)
    {
        computerTurnedOffScreen.SetActive(!enabled);
    }
}