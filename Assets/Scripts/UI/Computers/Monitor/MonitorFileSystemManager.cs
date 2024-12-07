using UnityEngine;

public class MonitorFileSystemManager : MonoBehaviour
{
    // Can be more than one instance in scene - for different computers

    [SerializeField]
    MonitorUI monitorUI;

    [SerializeField]
    private FolderSO rootFolderSO;

    public FolderSO RootFolderSO => rootFolderSO;

    private void Awake()
    {
        monitorUI.FileExplorer.InitializeFileExplorer(this);
    }
}