using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorAppUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text appNameTextField;

    [SerializeField]
    private Button appCloseButton;

    [SerializeField]
    private Button appMinimizeButton;

    private MonitorAppsManagerUI currentMonitorAppsManager;

    public MonitorAppsManagerUI CurrentMonitorAppManager => currentMonitorAppsManager;

    public bool IsMinimized { get; private set; }

    public MonitorAppsManagerUI.ApplicationType AppType { get; set; }

    private void Start()
    {
        appCloseButton.onClick.AddListener(CloseApp);
        if (appMinimizeButton != null)
        {
            appMinimizeButton.onClick.AddListener(() => currentMonitorAppsManager.TryMinimizeApp(AppType));
        }
    }

    public void InitializeApp(MonitorAppsManagerUI monitorAppManager, MonitorAppsManagerUI.ApplicationType appType)
    {
        currentMonitorAppsManager = monitorAppManager;
        AppType = appType;

        IsMinimized = false;
    }

    public void SetAppName(string appName)
    {
        gameObject.name = name = appName;
        appNameTextField.text = appName;
    }

    public void CloseApp()
    {
        Debug.Log($"Close app: {gameObject.name}");
        currentMonitorAppsManager.CloseApp(AppType);
    }

    public void CloseAppFromAppsManager()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetAppMinimized(bool minimized)
    {
        gameObject.SetActive(!minimized);
        IsMinimized = minimized;
    }

    public void BringAppToFront()
    {
        transform.SetAsLastSibling();
    }
}