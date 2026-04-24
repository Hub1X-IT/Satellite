using System.Collections.Generic;
using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
        DataContainerPasswordScreen,
        DoorApp,
        NotepadApp,
        PasswordCrackingApp,
        TracingApp,
    }

    [SerializeField]
    private Transform appsHolder;

    [SerializeField]
    private DataContainerPasswordScreenUI dataContainerPasswordScreenPrefab;
    [SerializeField]
    private DoorAppUI doorAppPrefab;
    [SerializeField]
    private NotepadAppUI notepadAppPrefab;
    [SerializeField]
    private PasswordCrackingAppUI passwordCrackingAppPrefab;
    [SerializeField]
    private TracingAppUI tracingAppPrefab;

    private Dictionary<ApplicationType, MonitorAppUI> openApps;

    private void Start()
    {
        openApps = new();
    }

    public MonitorAppUI OpenApplication(ApplicationType application)
    {
        MonitorAppUI instantiatedApp;

        switch (application)
        {
            default:
                return null;
            case ApplicationType.DataContainerPasswordScreen:
                instantiatedApp = Instantiate(dataContainerPasswordScreenPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.DoorApp:
                instantiatedApp = Instantiate(doorAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.NotepadApp:
                instantiatedApp = Instantiate(notepadAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.PasswordCrackingApp:
                instantiatedApp = Instantiate(passwordCrackingAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.TracingApp:
                instantiatedApp = Instantiate(tracingAppPrefab.gameObject, appsHolder).GetComponent<MonitorAppUI>();
                break;
        }
        instantiatedApp.InitializeApp(this, application);
        openApps[application] = instantiatedApp;
        return instantiatedApp;
    }

    public bool IsAppOpen(ApplicationType applicationType)
    {
        return openApps.ContainsKey(applicationType) && openApps[applicationType] != null;
    }

    public MonitorAppUI GetOpenApp(ApplicationType applicationType)
    {
        return openApps[applicationType];
    }

    public void ToggleMinimizeApp(ApplicationType applicationType)
    {
        MonitorAppUI monitorApp = openApps[applicationType];
        monitorApp.SetAppMinimized(!monitorApp.IsMinimized);
    }

    public void CloseApp(ApplicationType applicationType)
    {
        MonitorAppUI monitorApp = openApps[applicationType];
        monitorApp.CloseApp();
        openApps[applicationType] = null;
    }

    public void OpenDoorApp()
    {
        // Temporary method (idk whether it's used anywhere)
        DoorAppUI doorApp = OpenApplication(ApplicationType.DoorApp).GetComponent<DoorAppUI>();
    }
}