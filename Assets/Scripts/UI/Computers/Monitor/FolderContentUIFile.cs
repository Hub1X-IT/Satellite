using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : MonitorUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FileSO selfFileSO;

    private FolderContentUI parentFolderContentUI;

    protected override void Awake()
    {
        base.Awake();
        openButton.onClick.AddListener(OpenFileContent);
    }

    public void InitializeFile(FileSO fileSO, FolderContentUI parentFolderContentUI)
    {
        selfFileSO = fileSO;
        this.parentFolderContentUI = parentFolderContentUI;
        SetName(selfFileSO.SelfName);

        if (selfFileSO is FileStringSO)
        {
            // Set icon
        }
    }

    private void OpenFileContent()
    {
        if (selfFileSO is FileStringSO fileStringSO)
        {
            NotepadAppUI notepadAppUI = (NotepadAppUI)parentFolderContentUI.CurrentFileExplorer.CurrentMonitorAppsManager.
                OpenApplication(MonitorAppsManagerUI.ApplicationType.Notepad);
            notepadAppUI.InitializeNotepadAppUI(fileStringSO);
        }
    }
}