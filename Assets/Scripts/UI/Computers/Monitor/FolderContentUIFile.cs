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
        }
    }

    private void SetFileIcon()
    {
        if (selfFileSO is FileStringSO)
        {
            BaseDataContainerIcon = baseTextFileIcon;
            LockedDataContainerIcon = lockedTextFileIcon;
        }
        else
        {
            BaseDataContainerIcon = baseUnknownFileIcon;
            LockedDataContainerIcon = lockedUnknownFileIcon;
        }
    }
}