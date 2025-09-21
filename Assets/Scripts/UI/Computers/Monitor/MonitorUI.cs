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

    [SerializeField]
    private GameEventStartProgramDataSO startSputnikOSGameEvent;

    private bool isSputnikOSStarted;

    public FileExplorerUI FileExplorer => fileExplorer;

    private void Awake()
    {
        fileExplorer.CurrentMonitorAppsManager = appsManager;


        DetectionManager.DetectionOccured += () =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);

            isSputnikOSStarted = false;
        };

        ServerConnectionManager.ServerConnectionEnabled += (enabled) =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);

            if (!enabled)
            {
                isSputnikOSStarted = false;
            }
        };

        DetectionManager.ServerPowerEnabled += (enabled) =>
        {
            if (enabled)
            {
                SetMonitorEnabled(true);
                monitorStartupScreenUI.gameObject.SetActive(true);
                monitorStartupScreenUI.StartStartupScreen(null);
            }
            else
            {
                SetMonitorEnabled(false);
                monitorStartupScreenUI.DisableStartupScreen();

                isSputnikOSStarted = false;
            }
        };

        startSputnikOSGameEvent.EventRaised += (startProgramEventData) =>
        {
            if (!isSputnikOSStarted)
            {
                isSputnikOSStarted = true;
                monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
                startProgramEventData.Response?.Invoke(true, "Starting SputnikOS...");
            }
            else
            {
                startProgramEventData.Response?.Invoke(false, "SputnikOS is already started.");
            }

            // // This requires additional conditions in the event assignments above!
            // if (!monitorStartupScreenUI.IsStartupScreenStarted)
            // {
            //     monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
            // }
        };

        SetMonitorEnabled(true);
        isSputnikOSStarted = false;
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