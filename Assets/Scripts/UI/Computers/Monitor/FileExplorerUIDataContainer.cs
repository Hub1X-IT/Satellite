using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class FileExplorerUIDataContainer : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text nameTextField;

    [SerializeField]
    private Image dataContainerIconImage;

    [SerializeField]
    private Sprite baseDataContainerIcon;

    [SerializeField]
    private Sprite lockedDataContainerIcon;

    public RectTransform SelfRectTransform { get; private set; }

    private DataContainerSO selfDataContainerSO;

    protected FileExplorerUI currentFileExplorer;

    protected MonitorAppsManagerUI currentMonitorAppsManager;

    protected virtual void Awake()
    {
        SelfRectTransform = GetComponent<RectTransform>();
    }

    public void InitializeUIDataContainer(DataContainerSO dataContainerSO, FileExplorerUI currentFileExplorer)
    {
        this.currentFileExplorer = currentFileExplorer;
        currentMonitorAppsManager = currentFileExplorer.CurrentMonitorAppsManager;
        selfDataContainerSO = dataContainerSO;

        SetName(dataContainerSO.SelfName);
        UpdateDataContainerIcon();
    }

    public void SetName(string newName)
    {
        gameObject.name = name = nameTextField.text = newName;
    }

    public void DestroySelf()
    {
        SetName(name + "_Destroyed");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool TryOpenDataContainer()
    {
        if (selfDataContainerSO.IsLocked)
        {
            DataContainerPasswordScreenUI DataContainerPasswordScreen = currentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                ApplicationType.DataContainerPasswordScreen).GetComponent<DataContainerPasswordScreenUI>();
            DataContainerPasswordScreen.InitializeDataContainerPasswordScreen(selfDataContainerSO);
            DataContainerPasswordScreen.PasswordGuessed += UpdateDataContainerIcon;
            return false;
        }
        return true;
    }

    private void UpdateDataContainerIcon()
    {
        dataContainerIconImage.sprite = selfDataContainerSO.IsLocked && lockedDataContainerIcon != null ? lockedDataContainerIcon : baseDataContainerIcon;
    }
}