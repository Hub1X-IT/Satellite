using System.Collections.Generic;
using UnityEngine;

public class MonitorFileExplorerUI : MonoBehaviour
{
    [SerializeField]
    private SideMonitorUIFolder sideFolderUIPrefab;

    [SerializeField]
    private FolderContentUI folderContentUIPrefab;

    [SerializeField]
    private FolderSO rootFolderSO;

    [SerializeField]
    private SideMonitorUIFolder rootSideFolderUI;

    [SerializeField]
    private Transform folderContentHolder;

    private FolderContentUI currentFolderContentUI;

    public SideMonitorUIFolder SideFolderUIPrefab => sideFolderUIPrefab;

    private void Awake()
    {
        rootFolderSO.RefreshChildDataContainers();
    }

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

    public void OpenFolderContent(FolderSO folderSO, List<FolderSO> previousFolderSOList)
    {
        Destroy(currentFolderContentUI.gameObject);
        currentFolderContentUI = Instantiate(folderContentUIPrefab.gameObject, folderContentHolder).GetComponent<FolderContentUI>();
        currentFolderContentUI.InitializeFolderContent(folderSO, this, previousFolderSOList);
    }
}