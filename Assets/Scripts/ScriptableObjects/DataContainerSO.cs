using UnityEngine;

public class DataContainerSO : ScriptableObject
{
    [SerializeField]
    private string selfName;

    /*
    [SerializeField]
    private FolderSO parentFolder;

    private FolderSO previousParentFolder;
    */

    public string SelfName => selfName;

    /*
    private void OnValidate()
    {
        if (parentFolder != previousParentFolder)
        {
            if (previousParentFolder != null)
            {
                previousParentFolder.RemoveChildDataContainer(this);
            }
            if (parentFolder != null)
            {
                parentFolder.AddChildDataContainer(this);
            }
            previousParentFolder = parentFolder;
        }
    }
    */
}
