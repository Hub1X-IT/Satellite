using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileStringSO", order = 5)]
public class FileStringSO : FileSO
{
    [SerializeField]
    private string displayedFileContent;

    [Tooltip("Leave empty if you want it to be the same as the displayed file content.")]
    [SerializeField]
    private string realFileContent;

    public string DisplayedFileContent => displayedFileContent;

    public string RealFileContent
    {
        get
        {
            return realFileContent != null ? realFileContent : displayedFileContent;
        }
    }
}
