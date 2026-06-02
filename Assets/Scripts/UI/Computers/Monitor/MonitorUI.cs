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

    private Sequence detectionColorSequence;

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
            CommandResponseData responseData;

            if (!ServerConnectionManager.Instance.IsConnectionActive)
            {
                responseData = CommandResponseData.Failure("No server connection.");
            }
            else if (IsSputnikOSStarted)
            {
                responseData = CommandResponseData.Failure("SputnikOS is already started.");
            }
            else
            {
                IsSputnikOSStarted = true;
                monitorStartupScreenUI.StartSputnikOSStartupScreen(() =>
                {
                    monitorStartupScreenUI.DisableStartupScreen();
                    startProgramEventData.Response?.Invoke(CommandResponseData.Success("SputnikOS successfully started."));
                }, () => {
                    startProgramEventData.Response?.Invoke(CommandResponseData.Failure("SputnikOS startup aborted."));
                });

                responseData = CommandResponseData.InProgress("Starting SputnikOS...");
            }

            startProgramEventData.Response?.Invoke(responseData);

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
            AnimateDetectionChanceText(chance);
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

        // Kill any existing sequence to prevent stacking
        detectionColorSequence?.Kill();
        DOTween.Kill(detectionChanceText); // Kill any tweens on text

        // Create a smooth color loop animation for icon
        detectionColorSequence = DOTween.Sequence()
            .Append(detectionChanceIcon.DOColor(Color.red, 0.5f))
            .Join(detectionChanceText.DOColor(Color.red, 0.5f))
            .Append(detectionChanceIcon.DOColor(Color.white, 0.5f))
            .Join(detectionChanceText.DOColor(Color.white, 0.5f))
            .SetLoops(4, LoopType.Restart);

        // Same smooth animation for text - properly synchronized
        //detectionChanceText.DOColor(Color.red, 0.5f)
        //.SetLoops(-1, LoopType.Yoyo);

        // Stop animation after 3 seconds
        //Invoke(nameof(StopSetDetectionChanceColors), 4f);
        detectionColorSequence.OnComplete(() => StopSetDetectionChanceVisuals());
    }

    private void AnimateDetectionChanceText(int targetValue, float duration = 1f)
    {
        // Counter for smooth number increment
        int currentDisplayValue = int.Parse(detectionChanceText.text.Replace("%", ""));

        DOTween.To(
            () => currentDisplayValue,
            x =>
            {
                currentDisplayValue = x;
                detectionChanceText.text = currentDisplayValue.ToString() + "%";
            },
            targetValue,
            duration
        ).SetEase(Ease.OutQuad);
    }

    private void StopSetDetectionChanceVisuals()
    {
        detectionColorSequence?.Kill();
        DOTween.Kill(detectionChanceText); // Kill text tweens
        detectionChanceIcon.DOColor(Color.white, 0.2f);
        detectionChanceText.DOColor(Color.white, 0.2f);
    }
}