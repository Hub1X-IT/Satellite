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

    public bool IsSputnikOSStarted { get; private set; }

    public FileExplorerUI FileExplorer => fileExplorer;

    private void Awake()
    {
        fileExplorer.CurrentMonitorAppsManager = appsManager;


        DetectionManager.DetectionOccured += () =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);

            IsSputnikOSStarted = false;
        };

        ServerConnectionManager.ServerConnectionEnabled += (enabled) =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);

            if (!enabled)
            {
                IsSputnikOSStarted = false;
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

                IsSputnikOSStarted = false;
            }
        };

        startSputnikOSGameEvent.EventRaised += (startProgramEventData) =>
        {
            if (!ServerConnectionManager.IsConnectionActive)
            {
                startProgramEventData.Response?.Invoke(false, "No server connection.");
            }
            else if (IsSputnikOSStarted)
            {
                startProgramEventData.Response?.Invoke(false, "SputnikOS is already started.");
            }
            else
            {
                IsSputnikOSStarted = true;
                monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
                startProgramEventData.Response?.Invoke(true, "Starting SputnikOS...");
            }

            // // This requires additional conditions in the event assignments above!
            // if (!monitorStartupScreenUI.IsStartupScreenStarted)
            // {
            //     monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
            // }
        };

        SetMonitorEnabled(true);
        IsSputnikOSStarted = false;
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