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

    private MonitorFileExplorerUI currentMonitorFileExplorerUI;

    private VerticalLayoutGroup verticalLayoutGroup;

    private readonly Vector2 baseFolderSize = new(600f, 100f);

    private Vector2 currentSize;

    private float verticalChildOffset;

    protected override void Awake()
    {
        base.Awake();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        childFoldersButton.onClick.AddListener(ToggleChildFolders);
        folderContentButton.onClick.AddListener(ToggleFolderContent);
    }

    public void InitializeFolderUI(FolderSO folderSO, MonitorFileExplorerUI monitorFileExplorer)
    {
        selfFolderSO = folderSO;
        currentMonitorFileExplorerUI = monitorFileExplorer;
        SetName(selfFolderSO.SelfName);
        childFoldersButton.gameObject.SetActive(gameObject.activeSelf && selfFolderSO.HasChildFolders());
        childFoldersButton.image.sprite = selfFolderSO.AreChildFoldersShown ? childFoldersShownSprite : childFoldersHiddenSprite;
    }

    private void ToggleChildFolders()
    {
        selfFolderSO.AreChildFoldersShown = !selfFolderSO.AreChildFoldersShown;
        currentMonitorFileExplorerUI.RefreshFolders();
    }

    private void ToggleFolderContent()
    {
        Debug.Log(name + ": ToggleFolderContent");
    }

    public void RefreshFolderUI()
    {
        // Should only be called on the root folder.
        RefreshFolderUISize();
    }

    private void RefreshFolderUISize()
    {
        currentSize = baseFolderSize;
        verticalChildOffset = verticalLayoutGroup.spacing;

        MonitorUIDataContainer[] childDataContainersUI = GetComponentsInChildren<MonitorUIDataContainer>(true);

        foreach (var dataContainerUI in childDataContainersUI)
        {
            // Only include active folders that are direct children of this folder.
            if (dataContainerUI.gameObject.activeSelf && dataContainerUI.transform.parent == transform)
            {
                if (dataContainerUI is SideMonitorUIFolder folderUI)
                {
                    folderUI.RefreshFolderUISize();
                }
                AddChildDataContainerUI(dataContainerUI);
            }
        }

        SelfRectTransform.sizeDelta = currentSize;
    }

    private void AddChildDataContainerUI(MonitorUIDataContainer dataContainerUI)
    {
        currentSize.x = Mathf.Max(currentSize.x, dataContainerUI.SelfRectTransform.sizeDelta.x);
        currentSize.y += dataContainerUI.SelfRectTransform.sizeDelta.y + verticalChildOffset;
    }
}