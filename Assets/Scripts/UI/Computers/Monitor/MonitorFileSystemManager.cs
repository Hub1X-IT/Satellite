using UnityEngine;

public class MonitorFileSystemManager : MonoBehaviour
{
    // Can be more than one instance in scene - for different computers

    [SerializeField]
    MonitorAppsManagerUI monitorAppsManager;

    [SerializeField]
    private FolderSO rootFolderSO;

    public FolderSO RootFolderSO => rootFolderSO;

    private void Awake()
    {
        monitorAppsManager.MonitorFileSystemManager = this;
    }
}