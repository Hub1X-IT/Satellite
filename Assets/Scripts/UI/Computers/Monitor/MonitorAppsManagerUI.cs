using UnityEngine;

public class MonitorAppsManagerUI : MonoBehaviour
{
    public enum ApplicationType
    {
        FileExplorer,
        Notepad,
    }

    [SerializeField]
    private Transform appsHolder;

    /*
    [SerializeField]
    MonitorAppPrefabsSO monitorAppPrefabs;
    */

    [SerializeField]
    private FileExplorerUI fileExplorerAppPrefab;

    [SerializeField]
    private NewNotepadAppUI notepadAppPrefab;

    public MonitorFileSystemManager MonitorFileSystemManager { get; set; }


    public MonitorAppUI OpenApplication(ApplicationType application)
    {
        MonitorAppUI instantiatedApp;

        switch (application)
        {
            default:
                return null;
            case ApplicationType.FileExplorer:
                // instantiatedApp = Instantiate(monitorAppPrefabs.FileExplorerAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                instantiatedApp = Instantiate(fileExplorerAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                break;
            case ApplicationType.Notepad:
                // instantiatedApp = Instantiate(monitorAppPrefabs.NotepadAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                instantiatedApp = Instantiate(notepadAppPrefab, appsHolder).GetComponent<MonitorAppUI>();
                break;
        }
        instantiatedApp.InitializeApp(this);
        return instantiatedApp;
    }

    public void OpenFileExplorer()
    {
        FileExplorerUI fileExplorerUI = (FileExplorerUI)OpenApplication(ApplicationType.FileExplorer);
        fileExplorerUI.InitializeFileExplorer(MonitorFileSystemManager);
    }
}