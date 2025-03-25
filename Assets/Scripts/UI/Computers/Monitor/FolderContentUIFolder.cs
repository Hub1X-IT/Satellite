using UnityEngine;
using UnityEngine.UI;

public class FolderContentUIFolder : FileExplorerUIDataContainer
{
    [SerializeField]
    private Button openButton;

    [SerializeField]
    private Sprite baseFolderIcon;

    [SerializeField]
    private Sprite lockedFolderIcon;

    private FolderSO selfFolderSO;

    private FolderContentUI parentFolderContentUI;

    public void InitializeFolder(FolderSO folderSO, FolderContentUI parentFolderContentUI)
    {
        selfFolderSO = folderSO;
        SelfDataContainerSO = folderSO;
        this.parentFolderContentUI = parentFolderContentUI;

        BaseDataContainerIcon = baseFolderIcon;
        LockedDataContainerIcon = lockedFolderIcon;

        openButton.onClick.AddListener(OpenFolderContent);
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
