using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : FileExplorerUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FileSO selfFileSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFile(FileSO fileSO, FolderContentUI parentFolderContentUI)
    {
        openButton.onClick.AddListener(TryOpenFileContent);

        selfFileSO = fileSO;
        this.parentFolderContentUI = parentFolderContentUI;
        currentMonitorAppsManager = parentFolderContentUI.CurrentFileExplorer.CurrentMonitorAppsManager;

        if (selfFileSO is FileStringSO)
        {
            // Set icon
        }
    }

    private void TryOpenFileContent()
    {
        if (TryOpenDataContainer())
        {
            if (selfFileSO is FileStringSO fileStringSO)
            {
                NotepadAppUI notepadApp = currentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                    ApplicationType.NotepadApp).GetComponent<NotepadAppUI>();
                notepadApp.InitializeNotepadAppUI(fileStringSO);
            }
        }
    }
}