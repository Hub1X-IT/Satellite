using UnityEngine;

public class MonitorFileExplorerUI : MonoBehaviour
{
    [SerializeField]
    private SideMonitorUIFolder sideFolderUIPrefab;

    [SerializeField]
    private FolderSO rootFolderSO;

    [SerializeField]
    private SideMonitorUIFolder rootSideFolderUI;

    private void Start()
    {
        RefreshFolders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RefreshFolders();
        }
    }

    public void RefreshFolders()
    {
        MonitorUIDataContainer[] childDataContainersUI = rootSideFolderUI.GetComponentsInChildren<MonitorUIDataContainer>(true);

        foreach (var childDataContainer in childDataContainersUI)
        {
            if (childDataContainer.transform.parent == rootSideFolderUI.transform)
            {
                childDataContainer.DestroySelf();
            }
        }

        AddChildFolders(rootFolderSO, rootSideFolderUI);

        rootSideFolderUI.InitializeFolderUI(rootFolderSO, this);

        rootSideFolderUI.RefreshFolderUI();
    }

    private void AddChildFolders(FolderSO currentFolderSO, SideMonitorUIFolder currentUIFolder)
    {
        foreach (var dataContainerSO in currentFolderSO.ChildDataContainers)
        {
            if (dataContainerSO is FolderSO newFolderSO)
            {
                SideMonitorUIFolder newUIFolder = Instantiate(sideFolderUIPrefab.gameObject, currentUIFolder.transform).GetComponent<SideMonitorUIFolder>();

                newUIFolder.InitializeFolderUI(newFolderSO, this);

                AddChildFolders(newFolderSO, newUIFolder);

                newUIFolder.gameObject.SetActive(currentFolderSO.AreChildFoldersShown);
            }
        }
    }
}