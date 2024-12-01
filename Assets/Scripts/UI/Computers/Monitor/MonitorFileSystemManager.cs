using UnityEngine;

public class MonitorFileSystemManager : MonoBehaviour
{
    [SerializeField]
    MonitorAppsManagerUI monitorAppManager;

    [SerializeField]
    private FolderSO rootFolderSO;

    public FolderSO RootFolderSO => rootFolderSO;

    private void Awake()
    {
        monitorAppManager.MonitorFileSystemManager = this;
    }
}