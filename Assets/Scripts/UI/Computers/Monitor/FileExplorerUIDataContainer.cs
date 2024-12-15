using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class FileExplorerUIDataContainer : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text nameTextField;

    [SerializeField]
    private Image dataContainerIconImage;

    public RectTransform SelfRectTransform { get; private set; }

    protected DataContainerSO SelfDataContainerSO { private get; set; }

    protected Sprite BaseDataContainerIcon { private get; set; }

    protected Sprite LockedDataContainerIcon { private get; set; }

    protected FileExplorerUI CurrentFileExplorer { get; private set; }

    protected MonitorAppsManagerUI CurrentMonitorAppsManager { get; private set; }

    protected virtual void Awake()
    {
        SelfRectTransform = GetComponent<RectTransform>();
    }

    public virtual void InitializeUIDataContainer(FileExplorerUI currentFileExplorer)
    {
        // Should only be called after calling initialize method in child script!

        CurrentFileExplorer = currentFileExplorer;
        CurrentMonitorAppsManager = currentFileExplorer.CurrentMonitorAppsManager;

        SetName(SelfDataContainerSO.SelfName);
        UpdateIcon();
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
        if (SelfDataContainerSO.IsLocked)
        {
            DataContainerPasswordScreenUI DataContainerPasswordScreen = CurrentMonitorAppsManager.OpenApplication(MonitorAppsManagerUI.
                ApplicationType.DataContainerPasswordScreen).GetComponent<DataContainerPasswordScreenUI>();
            DataContainerPasswordScreen.InitializeDataContainerPasswordScreen(SelfDataContainerSO);
            DataContainerPasswordScreen.PasswordGuessed += OnDataContainerUnlocked;
            return false;
        }
        return true;
    }

    protected virtual void OnDataContainerUnlocked()
    {
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        dataContainerIconImage.sprite = SelfDataContainerSO.IsLocked && LockedDataContainerIcon != null ? 
            LockedDataContainerIcon : BaseDataContainerIcon;
    }
}