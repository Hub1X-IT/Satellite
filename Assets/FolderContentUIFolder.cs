using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFolder : MonitorUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FolderSO selfFolderSO;

    private MonitorFileExplorerUI currentMonitorFileExplorerUI;

    protected override void Awake()
    {
        base.Awake();
        // openButton.onClick.AddListener(OpenFolderContent);
    }


    public void InitializeFolder(FolderSO folderSO, MonitorFileExplorerUI monitorFileExplorerUI)
    {
        selfFolderSO = folderSO;
        currentMonitorFileExplorerUI = monitorFileExplorerUI;


    }

    private void OpenFolderContent()
    {

    }

}
