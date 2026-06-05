using UnityEngine;
using UnityEngine.UI;

public class TaskbarUI : MonoBehaviour
{
    [SerializeField]
    private MonitorAppsManagerUI monitorAppsManager;

    [SerializeField]
    private Button passwordCrackingAppButton;

    [SerializeField]
    private Button tracingAppButton;

    private void Start()
    {
        passwordCrackingAppButton.onClick.AddListener(() => OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType.PasswordCrackingApp));
        tracingAppButton.onClick.AddListener(() => OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType.TracingApp));

        // Temporary
        TracingManager.Instance.OnTracingStarted += () =>
        {
            if (monitorAppsManager.IsAppOpen(MonitorAppsManagerUI.ApplicationType.TracingApp))
            {
                MonitorAppUI tracingApp = monitorAppsManager.GetOpenApp(MonitorAppsManagerUI.ApplicationType.TracingApp);
                tracingApp.SetAppMinimized(false);
                monitorAppsManager.FocusApp(MonitorAppsManagerUI.ApplicationType.TracingApp);
            }
            else
            {
                OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType.TracingApp);
            }
        };
    }

    private void OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType applicationType)
    {
        if (monitorAppsManager.IsAppOpen(applicationType))
        {
            if (monitorAppsManager.IsAppMinimized(applicationType) || monitorAppsManager.IsAppFocused(applicationType))
            {
                monitorAppsManager.ToggleMinimizeApp(applicationType);
            }
            else
            {
                monitorAppsManager.FocusApp(applicationType);
            }
        }
        else
        {
            MonitorAppUI monitorApp = monitorAppsManager.OpenApplication(applicationType);

            // that will probably be moved somewhere else
            switch (applicationType)
            {
                default:
                    break;
                case MonitorAppsManagerUI.ApplicationType.PasswordCrackingApp:
                    PasswordCrackingAppUI passwordCrackingApp = monitorApp.GetComponent<PasswordCrackingAppUI>();
                    passwordCrackingApp.InitializePasswordCrackingApp();
                    break;
                case MonitorAppsManagerUI.ApplicationType.TracingApp:
                    TracingAppUI tracingApp = monitorApp.GetComponent<TracingAppUI>();
                    tracingApp.InitializeTracingApp();
                    break;
            }
        }
    }
}