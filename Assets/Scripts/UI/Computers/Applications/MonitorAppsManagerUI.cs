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

    // Temporary
    private PasswordCrackingAppUI currentPasswordCrackingApp;

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
        if (currentPasswordCrackingApp != null)
        {
            currentPasswordCrackingApp.gameObject.SetActive(true);
            currentPasswordCrackingApp.transform.SetAsLastSibling();
        }
        else
        {
            PasswordCrackingAppUI passwordCrackingApp = OpenApplication(ApplicationType.PasswordCrackingApp).GetComponent<PasswordCrackingAppUI>();
            passwordCrackingApp.InitializePasswordCrackingApp("Password cracking");
            currentPasswordCrackingApp = passwordCrackingApp;
        }
    }

    public void OpenDoorApp()
    {
        // Temporary method (idk whether it's used anywhere)
        DoorAppUI doorApp = OpenApplication(ApplicationType.DoorApp).GetComponent<DoorAppUI>();

    }
}