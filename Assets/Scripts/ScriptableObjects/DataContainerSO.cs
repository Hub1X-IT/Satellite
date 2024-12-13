using UnityEngine;

public class DataContainerSO : ScriptableObject
{
    [SerializeField]
    private string selfName;

    public string SelfName => selfName;

    public FolderSO ParentFolderSO { get; set; }

    [SerializeField]
    private bool isEncrypted;

    [SerializeField]
    private string dataContainerPassword;

    public bool IsEncrypted => isEncrypted;

    public string DataContainerPassword => dataContainerPassword;

    public bool IsLocked { get; set; }

    public void RefreshDataContainerSO()
    {
        IsLocked = IsEncrypted;
    }
}
