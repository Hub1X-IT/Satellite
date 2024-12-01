using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileStringSO", order = 5)]
public class FileStringSO : FileSO
{
    [SerializeField]
    private string fileContent;

    // May be temporary.
    [SerializeField]
    private bool canWriteToFile = true;

    public string FileContent => fileContent;

    public void SetFileContent(string content)
    {
        if (canWriteToFile)
        {
            fileContent = content;
        }
    }
}
