using System.Collections.Generic;
using UnityEngine;

public class FileExplorerUI : MonoBehaviour
{
    [SerializeField]
    private SideMonitorUIFolder sideFolderUIPrefab;

    [SerializeField]
    private FolderContentUI folderContentUIPrefab;

    [SerializeField]
    private SideMonitorUIFolder rootSideFolderUI;

    [SerializeField]
    private Transform folderContentHolder;

    private FolderContentUI currentFolderContentUI;

    public FolderSO RootFolderSO { get; private set; }

    public SideMonitorUIFolder SideFolderUIPrefab => sideFolderUIPrefab;

    public MonitorAppsManagerUI CurrentMonitorAppsManager { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RefreshSideFolders();
        }
    }

    public void InitializeFileExplorer(MonitorFileSystemManager fileSystemManager)
    {
        RootFolderSO = fileSystemManager.RootFolderSO;
        RootFolderSO.RefreshChildDataContainers();
        RefreshSideFolders();
    }

    public void RefreshSideFolders()
    {
        rootSideFolderUI.InitializeFolderUI(RootFolderSO, this);
        rootSideFolderUI.RefreshChildFolders();
    }

    public void ShowSideFolder(FolderSO folderSO)
    {
        FolderSO newFolderSO = folderSO.ParentFolderSO;
        while (newFolderSO != null)
        {
            newFolderSO.ShouldShowChildFolders = true;
            newFolderSO = newFolderSO.ParentFolderSO;
        }
        
        // To do: scroll to the folder

        RefreshSideFolders();
    }

    public void OpenFolderContent(FolderSO folderSO, List<FolderSO> previousFolderSOList)
    {
        if (currentFolderContentUI != null)
        {
            currentFolderContentUI.CloseFolderContentUI();
        }
        currentFolderContentUI = Instantiate(folderContentUIPrefab.gameObject, folderContentHolder).GetComponent<FolderContentUI>();
        currentFolderContentUI.InitializeFolderContentUI(folderSO, this, previousFolderSOList);
    }
}