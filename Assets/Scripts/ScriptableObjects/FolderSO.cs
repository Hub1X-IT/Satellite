using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FolderSO")]
public class FolderSO : DataContainerSO
{
    [SerializeField]
    private DataContainerSO[] childDataContainers;

    public DataContainerSO[] ChildDataContainers => childDataContainers;

    /*
    private HashSet<DataContainerSO> childDataContainersSet;

    public DataContainerSO[] ChildDataContainers => childDataContainersSet.ToArray();

    public void AddChildDataContainer(DataContainerSO dataContainer)
    {
        childDataContainersSet.Add(dataContainer);
        Debug.Log(childDataContainersSet.Count);
        Debug.Log(ChildDataContainers.Length);
    }

    public void RemoveChildDataContainer(DataContainerSO dataContainer)
    {
        childDataContainersSet.Remove(dataContainer);
    }
    */
}
