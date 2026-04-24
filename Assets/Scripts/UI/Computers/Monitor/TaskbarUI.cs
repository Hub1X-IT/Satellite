using UnityEngine;
using UnityEngine.UI;

public class TaskbarUI : MonoBehaviour
{
    [SerializeField]
    private MonitorAppsManagerUI monitorAppsManager;

    [SerializeField]
    private Button passwordCrackingAppButton;

    [SerializeField]
    private Button TracingAppButton;

    private void Start()
    {
        passwordCrackingAppButton.onClick.AddListener(() => OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType.PasswordCrackingApp));
        TracingAppButton.onClick.AddListener(() => OnAppButtonPressed(MonitorAppsManagerUI.ApplicationType.TracingApp));

        // Temporary
        TracingManager.Instance.OnTracingStarted += () =>
        {
            if (monitorAppsManager.IsAppOpen(MonitorAppsManagerUI.ApplicationType.TracingApp))
            {
                monitorAppsManager.GetOpenApp(MonitorAppsManagerUI.ApplicationType.TracingApp).SetAppMinimized(false);
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
            monitorAppsManager.ToggleMinimizeApp(applicationType);
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