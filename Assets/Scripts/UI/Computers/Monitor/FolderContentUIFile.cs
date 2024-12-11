using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : MonitorUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FileSO selfFileSO;

    private FolderContentUI parentFolderContentUI;

    private MonitorAppsManagerUI currentMonitorAppsManager;

    protected override void Awake()
    {
        base.Awake();
        openButton.onClick.AddListener(OpenFileContent);
    }

    public void InitializeFile(FileSO fileSO, FolderContentUI parentFolderContentUI)
    {
        selfFileSO = fileSO;
        this.parentFolderContentUI = parentFolderContentUI;
        currentMonitorAppsManager = parentFolderContentUI.CurrentFileExplorer.CurrentMonitorAppsManager;
        SetName(selfFileSO.SelfName);

        if (selfFileSO is FileStringSO)
        {
            // Set icon
        }
    }

    private void OpenFileContent()
    {
        if (selfFileSO.IsLocked)
        {
            FilePasswordScreenUI filePasswordScreen = currentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                ApplicationType.FilePasswordScreen).GetComponent<FilePasswordScreenUI>();
            filePasswordScreen.InitializeFilePasswordScreen(selfFileSO);
        }
        else if (selfFileSO is FileStringSO fileStringSO)
        {
            NotepadAppUI notepadApp = currentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                ApplicationType.NotepadApp).GetComponent<NotepadAppUI>();
            notepadApp.InitializeNotepadAppUI(fileStringSO);
        }
    }
}