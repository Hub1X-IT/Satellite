using UnityEngine;
using UnityEngine.UI;

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
                NotepadAppUI notepadApp = CurrentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                    ApplicationType.NotepadApp).GetComponent<NotepadAppUI>();
                notepadApp.InitializeNotepadAppUI(fileStringSO);
            }
            else if (selfFileSO is FileDoorAppShortcutSO fileAppShortcutSO)
            {
                DoorAppUI doorApp = CurrentMonitorAppsManager.
                    OpenApplication(fileAppShortcutSO.TriggeredApplicationType).GetComponent<DoorAppUI>();
                doorApp.InitializeDoorApp("Door app");
            }
            selfFileSO.TriggerOnOpenEvent();
        }
        else
        {
            selfFileSO.TriggerOnTryOpenEvent();
        }
    }

    override protected void OnDataContainerUnlocked()
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