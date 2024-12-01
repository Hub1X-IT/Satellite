using System.Collections.Generic;
using UnityEngine;

public class FileExplorerUI : MonitorAppUI
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

    private const string BaseAppName = "File explorer - ";

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

        SetAppName(BaseAppName + RootFolderSO.SelfName);
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

        SetAppName(BaseAppName + folderSO.SelfName);
    }
}