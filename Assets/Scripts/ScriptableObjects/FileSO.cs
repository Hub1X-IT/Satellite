using UnityEngine;

public class FileSO : DataContainerSO
{
    [SerializeField]
    private bool isEncrypted;

    [SerializeField]
    private string filePassword;

    public bool IsEncrypted => isEncrypted;

    public string FilePassword => filePassword;

    public bool IsLocked { get; set; }

    private void OnValidate()
    {
        IsLocked = IsEncrypted;
    }
}
