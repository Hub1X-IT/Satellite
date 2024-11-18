using UnityEngine;

[CreateAssetMenu(menuName = "Monitor file system/FileSO")]
public class FileSO : DataContainerSO
{
    [SerializeField]
    private string content;

    public string Content => content;
}
