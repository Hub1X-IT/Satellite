using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
        FilePasswordScreen,
        NotepadApp,
        PasswordCrackingApp,
    }

    [SerializeField]
    private Transform appsHolder;

    /*
    [SerializeField]
    MonitorAppPrefabsSO monitorAppPrefabs;
    */

    [SerializeField]
    private DataContainerPasswordScreenUI dataContainerPasswordScreenPrefab;

    [SerializeField]
    private NotepadAppUI notepadAppPrefab;

    [SerializeField]
    private PasswordCrackingAppUI passwordCrackingAppPrefab;

    public MonitorAppUI OpenApplication(ApplicationType application)
    {
        MonitorAppUI instantiatedApp;

        switch (application)
        {
            default:
                return null;
            case ApplicationType.FilePasswordScreen:
                instantiatedApp = Instantiate(dataContainerPasswordScreenPrefab, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.NotepadApp:
                // instantiatedApp = Instantiate(monitorAppPrefabs.NotepadAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                instantiatedApp = Instantiate(notepadAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.PasswordCrackingApp:
                instantiatedApp = Instantiate(passwordCrackingAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                break;
        }
        instantiatedApp.InitializeApp(this);
        return instantiatedApp;
    }
}