using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
        DataContainerPasswordScreen,
        DoorApp,
        NotepadApp,
        PasswordCrackingApp,
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
        }
        instantiatedApp.InitializeApp(this);
        return instantiatedApp;
    }

    public void OpenPasswordCracking()
    {
        // Method for temporary password cracking button - don't delete until the button is deleted!
        PasswordCrackingAppUI passwordCrackingApp = OpenApplication(ApplicationType.PasswordCrackingApp).GetComponent<PasswordCrackingAppUI>();
        passwordCrackingApp.InitializePasswordCrackingApp("Password cracking - test");
    }

    public void OpenDoorApp()
    {
        // Temporary method
        DoorAppUI doorApp = OpenApplication(ApplicationType.DoorApp).GetComponent<DoorAppUI>();

    }
}