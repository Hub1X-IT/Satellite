using UnityEngine;

public class FileSO : DataContainerSO
{
    [SerializeField]
    private bool isEncrypted;

    public bool IsEncrypted => isEncrypted;

    public bool IsLocked { get; set; }
}
