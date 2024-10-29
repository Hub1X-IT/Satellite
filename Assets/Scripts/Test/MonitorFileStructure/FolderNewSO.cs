using UnityEngine;

[CreateAssetMenu(fileName = "FolderNew", menuName = "Scriptable Objects/FolderNew")]
public class FolderNewSO : ScriptableObject
{
    [SerializeField]
    private string folderName;

    [SerializeField]
    private FolderNewSO parentFolder;

    [SerializeField]
    private DiskNewSO parentDisk;

    public FolderNew Folder { get; private set; }

    private void Awake()
    {
        Folder = new(null);
    }

    private void OnValidate()
    {
        Folder.SetName(folderName);
        if (parentFolder != null)
        {
            Folder.SetParentFolder(parentFolder.Folder);
        }
        else if (parentDisk != null)
        {
            Folder.SetParentDisk(parentDisk.Disk);
        }
    }

}
