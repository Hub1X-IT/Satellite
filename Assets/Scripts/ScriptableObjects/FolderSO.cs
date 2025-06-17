using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FolderSO", order = 1)]
public class FolderSO : DataContainerSO
{
    public bool ShouldShowChildFolders { get; set; }

    public bool IsFolderContentOpen { get; set; }

    [SerializeField]
    private DataContainerSO[] childDataContainers;

    public DataContainerSO[] ChildDataContainers => childDataContainers;

    [SerializeField]
    private bool shouldShowChildFoldersOnStart;

    public bool HasChildFolders()
    {
        foreach (var dataContainerSO in ChildDataContainers)
        {
            if (dataContainerSO is FolderSO)
            {
                return true;
            }
        }
        return false;
    }

    public void InitializeChildDataContainers()
    {
        foreach (var childDataContainerSO in ChildDataContainers)
        {
            childDataContainerSO.ParentFolderSO = this;
            childDataContainerSO.InitializeDataContainerSO();
            if (childDataContainerSO is FolderSO childFolderSO)
            {
                childFolderSO.InitializeChildDataContainers();
                childFolderSO.InitializeFolderSO();
            }
        }
    }

    public void InitializeFolderSO()
    {
        ShouldShowChildFolders = shouldShowChildFoldersOnStart;
        IsFolderContentOpen = false;
    }
}
