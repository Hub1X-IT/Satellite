using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderContentUI : MonoBehaviour
{
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

    public FileExplorerUI CurrentFileExplorer { get; private set; }

    private void Awake()
    {
        parentFolderButton.onClick.AddListener(() => CurrentFileExplorer.TryOpenFolderContent(selfFolderSO.ParentFolderSO, null, previousFolderSOList));
    }

    public void InitializeFolderContentUI(FolderSO folderSO, FileExplorerUI currentFileExplorer, List<FolderSO> previousFolderSOList)
    {
        selfFolderSO = folderSO;
        folderSO.IsFolderContentOpen = true;
        CurrentFileExplorer = currentFileExplorer;
        this.previousFolderSOList = previousFolderSOList;
        this.previousFolderSOList.Add(selfFolderSO);

        // Add all child data containers.
        foreach (var childDataContainerSO in selfFolderSO.ChildDataContainers)
        {
            FileExplorerUIDataContainer dataContainerUI = null;
            if (childDataContainerSO is FolderSO childFolderSO)
            {
                FolderContentUIFolder folderUI = Instantiate(folderContentUIFolderPrefab.gameObject, childObjectsHolder).GetComponent<FolderContentUIFolder>();
                folderUI.InitializeFolder(childFolderSO, this);
                dataContainerUI = folderUI;
            }
            else if (childDataContainerSO is FileSO childFileSO)
            {
                FolderContentUIFile fileUI = Instantiate(folderContentUIFilePrefab.gameObject, childObjectsHolder).GetComponent<FolderContentUIFile>();
                fileUI.InitializeFile(childFileSO, this);
                dataContainerUI = fileUI;
            }
            dataContainerUI.InitializeUIDataContainer(currentFileExplorer);
        }

        currentFileExplorer.ShowSideFolder(selfFolderSO);

        if (selfFolderSO == currentFileExplorer.RootFolderSO)
        {
            parentFolderButton.gameObject.SetActive(false);
        }
    }

    public void OpenNewFolderContent(FolderSO folderSO, FolderContentUIFolder folderContentUIFolder)
    {
        CurrentFileExplorer.TryOpenFolderContent(folderSO, folderContentUIFolder, previousFolderSOList);
    }

    public void CloseFolderContentUI()
    {
        selfFolderSO.IsFolderContentOpen = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}