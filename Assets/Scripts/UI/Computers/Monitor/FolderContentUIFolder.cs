using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFolder : FileExplorerUIDataContainer
{
    [SerializeField]
    private Button openButton;

    private FolderSO selfFolderSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFolder(FolderSO folderSO, FolderContentUI parentFolderContentUI)
    {
        selfFolderSO = folderSO;
        this.parentFolderContentUI = parentFolderContentUI;

        openButton.onClick.AddListener(() => parentFolderContentUI.OpenNewFolderContent(selfFolderSO, this));
    }
}
