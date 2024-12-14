using UnityEngine;
using UnityEngine.UI;

public class SideMonitorUIFolder : FileExplorerUIDataContainer
{
    [SerializeField]
    private Button childFoldersButton;

    [SerializeField]
    private Button folderContentButton;

    [SerializeField]
    private Sprite childFoldersHiddenSprite;

    [SerializeField]
    private Sprite childFoldersShownSprite;

    private FolderSO selfFolderSO;

    private VerticalLayoutGroup verticalLayoutGroup;

    private Vector2 baseFolderSize;

    public void InitializeFolderUI(FolderSO folderSO)
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        childFoldersButton.onClick.AddListener(ToggleChildFolders);
        folderContentButton.onClick.AddListener(ToggleFolderContent);

        baseFolderSize = SelfRectTransform.sizeDelta;

        selfFolderSO = folderSO;

        SetName(selfFolderSO.SelfName);

        childFoldersButton.gameObject.SetActive(gameObject.activeSelf && selfFolderSO.HasChildFolders());
        childFoldersButton.image.sprite = selfFolderSO.ShouldShowChildFolders ? childFoldersShownSprite : childFoldersHiddenSprite;
        childFoldersButton.image.rectTransform.sizeDelta = selfFolderSO.ShouldShowChildFolders ? new Vector2(40,25) : new Vector2(25,40);
    }

    private void ToggleChildFolders()
    {
        if (TryOpenDataContainer())
        {
            selfFolderSO.ShouldShowChildFolders = !selfFolderSO.ShouldShowChildFolders;
            currentFileExplorer.RefreshSideFolders();
        }
    }

    private void ToggleFolderContent()
    {
        currentFileExplorer.TryOpenFolderContent(selfFolderSO, this, new());
    }

    public void RefreshChildFolders()
    {
        // Should only be called on the root folder.

        FileExplorerUIDataContainer[] childDataContainersUI = GetComponentsInChildren<FileExplorerUIDataContainer>(true);
        foreach (var childDataContainer in childDataContainersUI)
        {
            if (childDataContainer.transform.parent == transform)
            {
                childDataContainer.DestroySelf();
            }
        }

        AddChildFolders(selfFolderSO);

        RefreshFolderUISize();
    }

    public void AddChildFolders(FolderSO currentFolderSO)
    {
        foreach (var dataContainerSO in currentFolderSO.ChildDataContainers)
        {
            if (dataContainerSO is FolderSO newFolderSO)
            {
                SideMonitorUIFolder newUIFolder = Instantiate(currentFileExplorer.SideFolderUIPrefab.gameObject,
                    transform).GetComponent<SideMonitorUIFolder>();

                newUIFolder.InitializeUIDataContainer(newFolderSO, currentFileExplorer);
                newUIFolder.InitializeFolderUI(newFolderSO);
                newUIFolder.AddChildFolders(newFolderSO);
                newUIFolder.gameObject.SetActive(currentFolderSO.ShouldShowChildFolders);
            }
        }
    }

    private void RefreshFolderUISize()
    {
        Vector2 currentSize = baseFolderSize;
        float verticalChildOffset = verticalLayoutGroup.spacing;

        FileExplorerUIDataContainer[] childDataContainersUI = GetComponentsInChildren<FileExplorerUIDataContainer>(true);

        foreach (var childDataContainerUI in childDataContainersUI)
        {
            // Only include active folders that are direct children of this folder.
            if (childDataContainerUI.gameObject.activeSelf && childDataContainerUI.transform.parent == transform)
            {
                if (childDataContainerUI is SideMonitorUIFolder childFolderUI)
                {
                    childFolderUI.RefreshFolderUISize();
                }
                // Add child data container UI.
                currentSize.x = Mathf.Max(currentSize.x, childDataContainerUI.SelfRectTransform.sizeDelta.x);
                currentSize.y += childDataContainerUI.SelfRectTransform.sizeDelta.y + verticalChildOffset;
            }
        }

        SelfRectTransform.sizeDelta = currentSize;
    }
}