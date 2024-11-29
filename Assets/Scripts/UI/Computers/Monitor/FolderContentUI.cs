using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderContentUI : MonoBehaviour
{
    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private FolderContentUIFolder folderContentUIFolderPrefab;

    private FolderSO selfFolderSO;

    private List<FolderSO> previousFolderSOList;

    private MonitorFileExplorerUI currentFileExplorer;

    private void Awake()
    {
        // closeButton.onClick.AddListener(CloseFolderContent);
    }

    public void InitializeFolderContent(FolderSO folderSO, MonitorFileExplorerUI fileExplorer, List<FolderSO> previousFolderSOList)
    {
        selfFolderSO = folderSO;
        currentFileExplorer = fileExplorer;
        this.previousFolderSOList = previousFolderSOList;
        this.previousFolderSOList.Add(selfFolderSO);
        foreach (var childDataContainer in selfFolderSO.ChildDataContainers)
        {
            
        }
    }

    private void AddChildDataContainer()
    {

    }

    private void CloseFolderContent()
    {

    }
}