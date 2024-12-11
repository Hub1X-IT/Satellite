using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
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

    // Temporary solution
    public void OpenPasswordCracking()
    {
        PasswordCrackingAppUI passwordCrackingApp = OpenApplication(ApplicationType.PasswordCrackingApp).GetComponent<PasswordCrackingAppUI>();
        passwordCrackingApp.InitializePasswordCrackingApp("Password cracking - test");
    }
}