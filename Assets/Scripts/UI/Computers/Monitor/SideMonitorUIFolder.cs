using UnityEngine;
using UnityEngine.UI;

public class SideMonitorUIFolder : MonitorUIDataContainer
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

    private FileExplorerUI currentMonitorFileExplorerUI;

    private VerticalLayoutGroup verticalLayoutGroup;

    private Vector2 baseFolderSize;

    protected override void Awake()
    {
        base.Awake();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        childFoldersButton.onClick.AddListener(ToggleChildFolders);
        folderContentButton.onClick.AddListener(ToggleFolderContent);

        baseFolderSize = SelfRectTransform.sizeDelta;
    }

    public void InitializeFolderUI(FolderSO folderSO, FileExplorerUI monitorFileExplorer)
    {
        selfFolderSO = folderSO;
        currentMonitorFileExplorerUI = monitorFileExplorer;

        SetName(selfFolderSO.SelfName);

        childFoldersButton.gameObject.SetActive(gameObject.activeSelf && selfFolderSO.HasChildFolders());
        childFoldersButton.image.sprite = selfFolderSO.ShouldShowChildFolders ? childFoldersShownSprite : childFoldersHiddenSprite;
    }

    private void ToggleChildFolders()
    {
        selfFolderSO.ShouldShowChildFolders = !selfFolderSO.ShouldShowChildFolders;
        currentMonitorFileExplorerUI.RefreshSideFolders();
    }

    private void ToggleFolderContent()
    {
        currentMonitorFileExplorerUI.OpenFolderContent(selfFolderSO, new());
    }

    public void RefreshChildFolders()
    {
        // Should only be called on the root folder.

        MonitorUIDataContainer[] childDataContainersUI = GetComponentsInChildren<MonitorUIDataContainer>(true);
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
                SideMonitorUIFolder newUIFolder = Instantiate(currentMonitorFileExplorerUI.SideFolderUIPrefab.gameObject,
                    transform).GetComponent<SideMonitorUIFolder>();

                newUIFolder.InitializeFolderUI(newFolderSO, currentMonitorFileExplorerUI);
                newUIFolder.AddChildFolders(newFolderSO);
                newUIFolder.gameObject.SetActive(currentFolderSO.ShouldShowChildFolders);
            }
        }
    }

    private void RefreshFolderUISize()
    {
        Vector2 currentSize = baseFolderSize;
        float verticalChildOffset = verticalLayoutGroup.spacing;

        MonitorUIDataContainer[] childDataContainersUI = GetComponentsInChildren<MonitorUIDataContainer>(true);

        foreach (var childDataContainerUI in childDataContainersUI)
        {
            // Only include active folders that are direct children of this folder.
            if (childDataContainerUI.gameObject.activeSelf && childDataContainerUI.transform.parent == transform)
            {
                if (childDataContainerUI is SideMonitorUIFolder childFolderUI)
                {
                    childFolderUI.RefreshFolderUISize();
                }
                // Add child data container UI
                currentSize.x = Mathf.Max(currentSize.x, childDataContainerUI.SelfRectTransform.sizeDelta.x);
                currentSize.y += childDataContainerUI.SelfRectTransform.sizeDelta.y + verticalChildOffset;
            }
        }

        SelfRectTransform.sizeDelta = currentSize;
    }
}