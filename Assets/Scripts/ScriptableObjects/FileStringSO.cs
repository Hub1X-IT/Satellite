using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileStringSO", order = 5)]
public class FileStringSO : FileSO
{
    [SerializeField]
    private string[] multilineFileContent;
    
    public string[] MultilineFileContent => multilineFileContent;
}
