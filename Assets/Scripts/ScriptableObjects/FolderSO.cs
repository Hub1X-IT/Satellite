using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FolderSO", order = 1)]
public class FolderSO : DataContainerSO
{
    public bool AreChildFoldersShown = true;

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
}
