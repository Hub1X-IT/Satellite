using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFile : MonitorUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FileSO selfFileSO;

    private MonitorFileExplorerUI currentMonitorFileExplorerUI;

    protected override void Awake()
    {
        base.Awake();
        openButton.onClick.AddListener(OpenFileContent);
    }


    public void InitializeFile(FileSO fileSO, MonitorFileExplorerUI monitorFileExplorerUI)
    {
        selfFileSO = fileSO;
        currentMonitorFileExplorerUI = monitorFileExplorerUI;


    }

    private void OpenFileContent()
    {

    }
}