using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFolder : FileExplorerUIDataContainer
{
    [SerializeField]
    private DoubleClickButtonUI openButton;

    [SerializeField]
    private Sprite folderIcon;

    private FolderSO selfFolderSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFolder(FolderSO folderSO, FolderContentUI parentFolderContentUI)
    {
        selfFolderSO = folderSO;
        SelfDataContainerSO = folderSO;
        this.parentFolderContentUI = parentFolderContentUI;

        DataContainerIcon = folderIcon;

        openButton.OnDoubleClick += OpenFolderContent;
    }

    private void OpenFolderContent()
    {
        parentFolderContentUI.OpenNewFolderContent(selfFolderSO, this);
    }

    protected override void OnDataContainerUnlocked()
    {
        base.OnDataContainerUnlocked();
        // CurrentFileExplorer.RefreshSideFolders();
        OpenFolderContent();
    }
}
