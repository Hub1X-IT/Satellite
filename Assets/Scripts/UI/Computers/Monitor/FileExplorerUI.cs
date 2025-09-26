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
    [SerializeField]
    private Transform sideFileExplorer;

    [SerializeField]
    private GameObject fileExplorerDisabledScreen;

    private FolderContentUI currentFolderContentUI;

    public FolderSO RootFolderSO { get; private set; }

    public SideMonitorUIFolder SideFolderUIPrefab => sideFolderUIPrefab;

    public MonitorAppsManagerUI CurrentMonitorAppsManager { get; set; }

    private bool isFileExplorerEnabled;

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.V))
        {
            RefreshSideFolders();
        }
        */
    }

    public void SetFileExplorerEnabled(bool enabled)
    {
        isFileExplorerEnabled = enabled;
        sideFileExplorer.gameObject.SetActive(enabled);
        folderContentHolder.gameObject.SetActive(enabled);
        fileExplorerDisabledScreen.SetActive(!enabled);
    }

    public void InitializeFileExplorer(MonitorFileSystemInitializer fileSystemManager)
    {
        RootFolderSO = fileSystemManager.RootFolderSO;
        RootFolderSO.InitializeChildDataContainers();
        RootFolderSO.InitializeFolderSO();
        RefreshSideFolders();
    }

    public void RefreshSideFolders()
    {
        rootSideFolderUI.InitializeFolderUI(RootFolderSO);
        rootSideFolderUI.InitializeUIDataContainer(this);
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

    public void TryOpenFolderContent(FolderSO folderSO, FileExplorerUIDataContainer dataContainerUI, List<FolderSO> previousFolderSOList)
    {
        if (dataContainerUI == null || dataContainerUI.TryOpenDataContainer())
        {
            if (currentFolderContentUI != null)
            {
                currentFolderContentUI.CloseFolderContentUI();
            }
            currentFolderContentUI = Instantiate(folderContentUIPrefab.gameObject, folderContentHolder).GetComponent<FolderContentUI>();
            currentFolderContentUI.InitializeFolderContentUI(folderSO, this, previousFolderSOList);
        }
    }
}