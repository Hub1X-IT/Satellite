using UnityEngine;

public class DataContainerSO : ScriptableObject
{
    [SerializeField]
    private string selfName;

    public string SelfName => selfName;

    public FolderSO ParentFolderSO { get; set; }

    [SerializeField]
    private bool isEncrypted;

    public bool IsEncrypted => isEncrypted;

    public string DataContainerPassword { get; set; }

    public bool IsLocked { get; set; }

    public virtual void InitializeDataContainerSO()
    {
        IsLocked = IsEncrypted;
    }
}
