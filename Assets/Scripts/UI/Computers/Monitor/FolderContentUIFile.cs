using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : MonitorUIDataContainer
{
    [SerializeField]
    private Button openButton;

    [SerializeField]
    private NewNotepadAppUI notepadAppUIPrefab;

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
            // May be a temporary solution.
            NewNotepadAppUI notepadAppUI = (NewNotepadAppUI)parentFolderContentUI.CurrentFileExplorer.CurrentMonitorAppManager.
                OpenApplication(MonitorAppsManagerUI.ApplicationType.Notepad);
            notepadAppUI.InitializeNotepadAppUI(fileStringSO);
        }
    }
}