using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FolderSO", order = 1)]
public class FolderSO : DataContainerSO
{
    public bool ShouldShowChildFolders { get; set; }

    [SerializeField]
    private DataContainerSO[] childDataContainers;

    public DataContainerSO[] ChildDataContainers => childDataContainers;

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

    public void RefreshChildDataContainers()
    {
        foreach (var childDataContainerSO in ChildDataContainers)
        {
            childDataContainerSO.ParentFolderSO = this;
            childDataContainerSO.RefreshDataContainerSO();
            if (childDataContainerSO is FolderSO childFolderSO)
            {
                childFolderSO.RefreshChildDataContainers();
                childFolderSO.RefreshFolderSO();
            }
        }
    }

    public void RefreshFolderSO()
    {
        ShouldShowChildFolders = false;
    }
}
