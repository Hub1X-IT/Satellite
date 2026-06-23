using UnityEngine;

public class FolderContentUIFile : FileExplorerUIDataContainer
{
    [SerializeField]
    private DoubleClickButtonUI openButton;

    [SerializeField]
    private Sprite unknownFileIcon;

    [SerializeField]
    private Sprite textFileIcon;

    [SerializeField]
    private Sprite doorAppShortcutFileIcon;

    private FileSO selfFileSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFile(FileSO fileSO, FolderContentUI parentFolderContentUI)
    {
        selfFileSO = fileSO;
        SelfDataContainerSO = fileSO;
        this.parentFolderContentUI = parentFolderContentUI;

        SetFileIcon();

        openButton.OnDoubleClick += TryOpenFileContent;
    }

    private void TryOpenFileContent()
    {
        if (TryOpenDataContainer())
        {
            if (selfFileSO is FileStringSO fileStringSO)
            {
                if (CurrentMonitorAppsManager.TryOpenApp(MonitorAppsManagerUI.ApplicationType.NotepadApp, out var monitorAppUI))
                {
                    var notepadApp = monitorAppUI.GetComponent<NotepadAppUI>();
                    notepadApp.InitializeNotepadAppUI(fileStringSO);
                }
            }
            else if (selfFileSO is FileDoorAppShortcutSO)
            {
                if (CurrentMonitorAppsManager.TryOpenApp(MonitorAppsManagerUI.ApplicationType.DoorApp, out var monitorAppUI))
                {
                    var doorApp = monitorAppUI.GetComponent<DoorAppUI>();
                    doorApp.InitializeDoorApp("Door Control System");
                }
            }
        }
    }

    protected override void OnDataContainerUnlocked()
    {
        base.OnDataContainerUnlocked();
        TryOpenFileContent();
    }

    private void SetFileIcon()
    {
        if (selfFileSO is FileStringSO)
        {
            DataContainerIcon = textFileIcon;
        }
        else if (selfFileSO is FileDoorAppShortcutSO)
        {
            DataContainerIcon = doorAppShortcutFileIcon;
        }
        else
        {
            DataContainerIcon = unknownFileIcon;
        }
    }
}