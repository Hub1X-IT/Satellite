using UnityEngine;

public class MonitorFileExplorerUI : MonoBehaviour
{
    [SerializeField]
    private SideMonitorUIFolder sideFolderUIPrefab;

    [SerializeField]
    private FolderContentUI folderContentUIPrefab;

    [SerializeField]
    private Transform folderContentHolder;

    [SerializeField]
    private FolderSO rootFolderSO;

    [SerializeField]
    private SideMonitorUIFolder rootSideFolderUI;

    public SideMonitorUIFolder SideFolderUIPrefab => sideFolderUIPrefab;

    private void Start()
    {
        RefreshSideFolders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RefreshSideFolders();
        }
    }

    public void RefreshSideFolders()
    {
        rootSideFolderUI.InitializeFolderUI(rootFolderSO, this);
        rootSideFolderUI.RefreshChildFolders();
    }

    private void OpenFolderContent(FolderSO folderSO)
    {
        FolderContentUI folderContentUI = Instantiate(folderContentUIPrefab.gameObject, folderContentHolder).GetComponent<FolderContentUI>();
    }
}