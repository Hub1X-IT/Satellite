using System.Collections.Generic;
using UnityEngine;

public class MonitorFileExplorerUI : MonoBehaviour
{
    [SerializeField]
    private MonitorUIFolder folderUIPrefab;

    [SerializeField]
    private MonitorUIFile fileUIPrefab;

    [SerializeField]
    private FolderSO mainParentFolder;

    [SerializeField]
    private MonitorUIFolder mainParentFolderUI;


    private void Start()
    {
        RefreshFolders();
    }

    private void RefreshFolders()
    {
        mainParentFolderUI.SetUIName(mainParentFolder.SelfName);
        AddChildDataContainters(mainParentFolder, mainParentFolderUI);
        mainParentFolderUI.RefreshFolderUISize();
    }

    private void AddChildDataContainters(FolderSO folderSO, MonitorUIFolder currentParentUIFolder)
    {
        HashSet<MonitorUIDataContainer> dataContainerUISet = new();

        foreach (var dataContainer in folderSO.ChildDataContainers)
        {
            if (dataContainer.GetType() == typeof(FolderSO))
            {
                FolderSO newFolderSO = (FolderSO)dataContainer;
                MonitorUIFolder newParentUIFolder = Instantiate(folderUIPrefab.gameObject, currentParentUIFolder.transform).GetComponent<MonitorUIFolder>();
                newParentUIFolder.SetUIName(newFolderSO.SelfName);

                // newParentUIFolder.gameObject.name = newParentUIFolder.name = newFolderSO.SelfName;

                AddChildDataContainters(newFolderSO, newParentUIFolder);

                dataContainerUISet.Add(newParentUIFolder);
            }
            else if (dataContainer.GetType() == typeof(FileStringSO))
            {
                FileStringSO fileSO = (FileStringSO)dataContainer;
                MonitorUIFile fileUI = Instantiate(fileUIPrefab.gameObject, currentParentUIFolder.transform).GetComponent<MonitorUIFile>();
                fileUI.SetUIName(fileSO.SelfName);
                fileUI.SetFileContent(fileSO.FileContent.Content);

                dataContainerUISet.Add(fileUI);
            }
        }
    }
}