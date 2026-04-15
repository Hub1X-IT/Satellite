using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    [SerializeField]
    private FileExplorerUI fileExplorer;

    [SerializeField]
    private MonitorAppsManagerUI appsManager;

    [SerializeField]
    private MonitorStartupScreenUI monitorStartupScreenUI;

    [SerializeField]
    private GameObject computerTurnedOffScreen;

    [SerializeField]
    private GameEventStartProgramDataSO startSputnikOSGameEvent;

    [SerializeField]
    private TMP_Text detectionChanceText;
    
    [SerializeField]
    private Image detectionChanceIcon;

    public bool IsSputnikOSStarted { get; private set; }

    public FileExplorerUI FileExplorer => fileExplorer;

    private void Awake()
    {
        fileExplorer.CurrentMonitorAppsManager = appsManager;

        IsSputnikOSStarted = false;
    }

    private void Start()
    {
        ServerConnectionManager.Instance.OnServerConnectionStateChanged += (isConnected) =>
        {
            monitorStartupScreenUI.gameObject.SetActive(true);
            monitorStartupScreenUI.StartStartupScreen(null);

            if (!isConnected)
            {
                IsSputnikOSStarted = false;
            }
        };

        PowerManager.Instance.OnPowerStateChanged += (isPowerOn) =>
        {
            if (isPowerOn)
            {
                SetMonitorEnabled(true);
                monitorStartupScreenUI.gameObject.SetActive(true);
                monitorStartupScreenUI.StartStartupScreen(null);
            }
            else
            {
                SetMonitorEnabled(false);
                monitorStartupScreenUI.DisableStartupScreen();

                IsSputnikOSStarted = false;
            }
        };

        startSputnikOSGameEvent.EventRaised += (startProgramEventData) =>
        {
            if (!ServerConnectionManager.Instance.IsConnectionActive)
            {
                startProgramEventData.Response?.Invoke(false, "No server connection.");
            }
            else if (IsSputnikOSStarted)
            {
                startProgramEventData.Response?.Invoke(false, "SputnikOS is already started.");
            }
            else
            {
                IsSputnikOSStarted = true;
                monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
                startProgramEventData.Response?.Invoke(true, "Starting SputnikOS...");
            }

            // // This requires additional conditions in the event assignments above!
            // if (!monitorStartupScreenUI.IsStartupScreenStarted)
            // {
            //     monitorStartupScreenUI.StartSputnikOSStartupScreen(() => monitorStartupScreenUI.DisableStartupScreen());
            // }
        };

        SetMonitorEnabled(true);
        monitorStartupScreenUI.StartStartupScreen(null);

        DetectionManager.Instance.OnDetectionChanceChanged += (chance) =>
        {
            SetDetectionChanceVisual();
        };
    }

    private void SetMonitorEnabled(bool enabled)
    {
        computerTurnedOffScreen.SetActive(!enabled);
    }

    private void SetDetectionChanceVisual()
    {
        detectionChanceText.text = DetectionManager.Instance.CurrentDetectionChance.ToString() + "%";  
        InvokeRepeating(nameof(SetDetectionChanceColors), 0f, 0.1f * Time.deltaTime);
        Invoke(nameof(StopSetDetectionChanceColors), 3f);
    }

    private void SetDetectionChanceColors()
    {
        detectionChanceIcon.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 1f, 1f));
        detectionChanceText.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * 1f, 1f));
    }
    
    private void StopSetDetectionChanceColors()
    {
        CancelInvoke(nameof(SetDetectionChanceColors));
        detectionChanceIcon.color = Color.white;
        detectionChanceText.color = Color.white;
    }
}