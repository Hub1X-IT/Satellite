using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileStringSO", order = 5)]
public class FileStringSO : FileSO
{
    [SerializeField]
    private string fileContent;

    /*
    [SerializeField]
    private string[] multilineFileContent;
    */

    [SerializeField]
    private bool shouldCompressContent;

    public string FileContent => fileContent;

    /*
    public string[] MultilineFileContent => multilineFileContent;
    */

    public bool ShouldCompressContent => shouldCompressContent;
}
