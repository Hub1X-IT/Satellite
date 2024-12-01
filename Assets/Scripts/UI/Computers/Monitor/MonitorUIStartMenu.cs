using UnityEngine;
using UnityEngine.UI;

public class MonitorUIStartMenu : MonoBehaviour
{
    [SerializeField]
    private MonitorAppsManagerUI monitorAppsManager;

    [SerializeField]
    private Button fileExplorerButton;

    [SerializeField]
    private bool isStartMenuEnabled = false;

    private void Awake()
    {
        fileExplorerButton.onClick.AddListener(monitorAppsManager.OpenFileExplorer);

        SetStartMenuEnabled(isStartMenuEnabled);
    }

    public void ToggleStartMenu()
    {
        SetStartMenuEnabled(!isStartMenuEnabled);
    }

    public void SetStartMenuEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
        isStartMenuEnabled = enabled;
    }
}