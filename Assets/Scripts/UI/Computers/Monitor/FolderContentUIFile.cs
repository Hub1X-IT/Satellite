using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : FileExplorerUIDataContainer
{
    [SerializeField]
    private Button openButton;

    [SerializeField]
    private Sprite baseUnknownFileIcon;

    [SerializeField]
    private Sprite lockedUnknownFileIcon;

    [SerializeField]
    private Sprite baseTextFileIcon;

    [SerializeField]
    private Sprite lockedTextFileIcon;

    [SerializeField]
    private Sprite baseDoorAppShortcutFileIcon;

    [SerializeField]
    private Sprite lockedDoorAppShortcutFileIcon;

    private FileSO selfFileSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFile(FileSO fileSO, FolderContentUI parentFolderContentUI)
    {
        selfFileSO = fileSO;
        SelfDataContainerSO = fileSO;
        this.parentFolderContentUI = parentFolderContentUI;

        SetFileIcon();

        openButton.onClick.AddListener(TryOpenFileContent);
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

    private void SetFileIcon()
    {
        if (selfFileSO is FileStringSO)
        {
            BaseDataContainerIcon = baseTextFileIcon;
            LockedDataContainerIcon = lockedTextFileIcon;
        }
        else if (selfFileSO is FileDoorAppShortcutSO)
        {
            BaseDataContainerIcon = baseDoorAppShortcutFileIcon;
            LockedDataContainerIcon = lockedDoorAppShortcutFileIcon;
        }
        else
        {
            BaseDataContainerIcon = baseUnknownFileIcon;
            LockedDataContainerIcon = lockedUnknownFileIcon;
        }
    }
}