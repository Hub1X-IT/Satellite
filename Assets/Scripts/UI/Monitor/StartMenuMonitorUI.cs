using UnityEngine;
using UnityEngine.UI;

public class StartMenuMonitorUI : MonoBehaviour
{
    private MonitorUI monitorUI;

    [SerializeField]
    private Button fileExplorerButton;

    private void Awake()
    {
        monitorUI = GetComponentInParent<MonitorUI>();

        fileExplorerButton.onClick.AddListener(() => { });
    }

}
