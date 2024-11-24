using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FolderSO", order = 1)]
public class FolderSO : DataContainerSO
{
    [SerializeField]
    private DataContainerSO[] childDataContainers;

    public DataContainerSO[] ChildDataContainers => childDataContainers;
}
