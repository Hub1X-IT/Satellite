using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIDataContainer : MonitorUIDataContainer
{
    [SerializeField]
    protected Button openButton;

    protected MonitorFileExplorerUI currentMonitorFileExplorerUI;

    protected override void Awake()
    {
        base.Awake();
        openButton.onClick.AddListener(OpenDataContainer);
    }

    public void InitializeDataContainer(MonitorFileExplorerUI monitorFileExplorerUI)
    {
        currentMonitorFileExplorerUI = monitorFileExplorerUI;


    }

    private void OpenDataContainer()
    {

    }
}