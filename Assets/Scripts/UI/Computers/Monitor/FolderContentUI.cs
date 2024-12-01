using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderContentUI : MonoBehaviour
{
    /*
    [SerializeField]
    private Button closeButton;
    */

    [SerializeField]
    private Button parentFolderButton;

    [SerializeField]
    private Transform childObjectsHolder;

    [SerializeField]
    private FolderContentUIFolder folderContentUIFolderPrefab;

    [SerializeField]
    private FolderContentUIFile folderContentUIFilePrefab;

    private FolderSO selfFolderSO;

    private List<FolderSO> previousFolderSOList;

    private FileExplorerUI currentFileExplorer;

    public FileExplorerUI CurrentFileExplorer => currentFileExplorer;

    private void Awake()
    {
        // closeButton.onClick.AddListener(CloseFolderContentUI);
        parentFolderButton.onClick.AddListener(() => currentFileExplorer.OpenFolderContent(selfFolderSO.ParentFolderSO, previousFolderSOList));
    }

    public void InitializeFolderContentUI(FolderSO folderSO, FileExplorerUI fileExplorer, List<FolderSO> previousFolderSOList)
    {
        selfFolderSO = folderSO;
        currentFileExplorer = fileExplorer;
        this.previousFolderSOList = previousFolderSOList;
        this.previousFolderSOList.Add(selfFolderSO);

        // Add all child data containers.
        foreach (var childDataContainer in selfFolderSO.ChildDataContainers)
        {
            if (childDataContainer is FolderSO childFolderSO)
            {
                Instantiate(folderContentUIFolderPrefab.gameObject, childObjectsHolder)
                    .GetComponent<FolderContentUIFolder>().InitializeFolder(childFolderSO, this);
            }
            else if (childDataContainer is FileSO childFileSO)
            {
                Instantiate(folderContentUIFilePrefab.gameObject, childObjectsHolder)
                    .GetComponent<FolderContentUIFile>().InitializeFile(childFileSO, this);
            }
        }

        currentFileExplorer.ShowSideFolder(selfFolderSO);

        if (selfFolderSO == fileExplorer.RootFolderSO)
        {
            parentFolderButton.gameObject.SetActive(false);
        }
    }

    public void OpenNewFolderContent(FolderSO folderSO)
    {
        currentFileExplorer.OpenFolderContent(folderSO, previousFolderSOList);
    }

    public void CloseFolderContentUI()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}