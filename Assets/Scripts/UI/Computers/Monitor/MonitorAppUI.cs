using UnityEngine;

public class MonitorAppUI : MonoBehaviour
{
    [SerializeField]
    private AppTitleBarUI titleBar;

    private MonitorAppsManagerUI currentMonitorAppManager;

    public MonitorAppsManagerUI CurrentMonitorAppManager => currentMonitorAppManager;

    public void InitializeApp(MonitorAppsManagerUI monitorAppManager)
    {
        currentMonitorAppManager = monitorAppManager;
        titleBar.AppCloseTriggered += CloseApp;
    }

    protected void SetAppName(string appName)
    {
        titleBar.SetAppName(appName);
    }

    public void CloseApp()
    {
        Debug.Log("close app");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}