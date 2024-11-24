using UnityEngine;
using UnityEngine.UI;

public class MonitorUIFolder : MonitorUIDataContainer
{
    private VerticalLayoutGroup verticalLayoutGroup;

    private readonly Vector2 baseFolderSize = new(600f, 80f);

    private Vector2 currentSize;

    private float verticalChildOffset;

    protected override void Awake()
    {
        base.Awake();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    public void RefreshFolderUISize()
    {
        currentSize = baseFolderSize;
        verticalChildOffset = verticalLayoutGroup.spacing;

        MonitorUIDataContainer[] childDataContainersUI = GetComponentsInChildren<MonitorUIDataContainer>(true);

        foreach (var childDataContainer in childDataContainersUI)
        {
            // Only include folders that are direct children of this folder.
            if (childDataContainer.transform.parent == transform)
            {
                if (childDataContainer.GetType() == typeof(MonitorUIFolder))
                {
                    MonitorUIFolder monitorUIFolder = (MonitorUIFolder)childDataContainer;
                    monitorUIFolder.RefreshFolderUISize();
                }
                AddChildDataContainerUI(childDataContainer);
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