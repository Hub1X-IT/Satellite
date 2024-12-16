using UnityEngine;

public class MonitorFileSystemInitializer : MonoBehaviour
{
    // Can be more than one instance in scene - for different computers

    [SerializeField]
    MonitorUI monitorUI;

    [SerializeField]
    private FolderSO rootFolderSO;

    [SerializeField]
    private PossiblePasswordsSO possiblePasswordsSO;

    public FolderSO RootFolderSO => rootFolderSO;

    private void Awake()
    {
        possiblePasswordsSO.InitializePossiblePasswords();
    }

    private void Start()
    {
        monitorUI.FileExplorer.InitializeFileExplorer(this);
    }
}