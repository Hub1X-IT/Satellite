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
    

    private void Awake()
    {
        RefreshFolders();
    }

    private void RefreshFolders()
    {
        mainParentFolderUI.SetFolderName(mainParentFolder.SelfName);
        Transform currentParentObject = mainParentFolderUI.transform;
        AddChildDataContainters(mainParentFolder, currentParentObject);
    }

    private void AddChildDataContainters(FolderSO folder, Transform currentParentObject)
    {
        foreach (var dataContainer in folder.ChildDataContainers)
        {
            /*
            Debug.Log(dataContainer.name);
            Debug.Log(dataContainer.GetType().ToString());
            */
            if (dataContainer.GetType() == typeof(FolderSO))
            {
                Transform newParentObject = Instantiate(folderUIPrefab.gameObject, currentParentObject).transform;
                FolderSO folderSO = (FolderSO)dataContainer;
                newParentObject.GetComponent<MonitorUIFolder>().SetFolderName(folderSO.SelfName);
                AddChildDataContainters(folderSO, newParentObject);
            }
            else if (dataContainer.GetType() == typeof(FileSO))
            {
                MonitorUIFile fileUI = Instantiate(fileUIPrefab.gameObject, currentParentObject).GetComponent<MonitorUIFile>();
                FileSO fileSO = (FileSO)dataContainer;
                fileUI.SetFileName(fileSO.SelfName);
                fileUI.SetFileContent(fileSO.Content);
            }
        }
    }
}