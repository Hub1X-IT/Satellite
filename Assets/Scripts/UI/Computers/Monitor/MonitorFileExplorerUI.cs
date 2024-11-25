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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RefreshFolders();
        }
    }

    private void RefreshFolders()
    {
        MonitorUIDataContainer[] childDataContainersUI = mainParentFolderUI.GetComponentsInChildren<MonitorUIDataContainer>(true);

        foreach (var childDataContainer in childDataContainersUI)
        {
            if (childDataContainer.transform.parent == mainParentFolderUI.transform)
            {
                Destroy(childDataContainer.gameObject);
            }
        }

        AddChildDataContainters(mainParentFolder, mainParentFolderUI);

        mainParentFolderUI.SetUIName(mainParentFolder.SelfName);

        StartCoroutine(mainParentFolderUI.RefreshFolderUIOnNextFrame());
    }

    private void AddChildDataContainters(FolderSO currentFolderSO, MonitorUIFolder currentParentUIFolder)
    {
        foreach (var dataContainer in currentFolderSO.ChildDataContainers)
        {
            MonitorUIDataContainer newMonitorUIDataContainer = null;
            DataContainerSO newDataContainerSO = null;

            if (dataContainer.GetType() == typeof(FolderSO))
            {
                FolderSO newFolderSO = (FolderSO)dataContainer;
                MonitorUIFolder newParentUIFolder = Instantiate(folderUIPrefab.gameObject, currentParentUIFolder.transform).GetComponent<MonitorUIFolder>();

                AddChildDataContainters(newFolderSO, newParentUIFolder);

                newParentUIFolder.gameObject.SetActive(newFolderSO.IsOpen);

                newMonitorUIDataContainer = newParentUIFolder;
                newDataContainerSO = newFolderSO;
            }
            else if (dataContainer.GetType() == typeof(FileStringSO))
            {
                FileStringSO newFileSO = (FileStringSO)dataContainer;
                MonitorUIFile newFileUI = Instantiate(fileUIPrefab.gameObject, currentParentUIFolder.transform).GetComponent<MonitorUIFile>();

                newFileUI.SetFileContent(newFileSO.FileContent.Content);

                newMonitorUIDataContainer = newFileUI;
                newDataContainerSO = newFileSO;
            }

            newMonitorUIDataContainer.SetUIName(newMonitorUIDataContainer.gameObject.name = newMonitorUIDataContainer.name = newDataContainerSO.SelfName);
        }
    }
}