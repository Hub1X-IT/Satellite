using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TracingAppUI : MonoBehaviour
{
    private MonitorAppUI monitorAppUI;

    private const string AppName = "Trace Tracker";

    [SerializeField]
    private TMP_Text remainingTimeTextField;
    [SerializeField]
    private Slider trackingProgress;
    [SerializeField]
    private GameObject inactiveTrackingNote;
    [SerializeField]
    private GameObject activeTrackingNote;

    public void InitializeTracingApp()
    {
        monitorAppUI = GetComponent<MonitorAppUI>();
        monitorAppUI.SetAppName(AppName);

        PowerManager.Instance.OnPowerStateChanged += (isPowerEnabled) =>
        {
            // May need to move this to MonitorAppsManagerUI

            if (!isPowerEnabled && monitorAppUI != null)
            {
                monitorAppUI.CloseApp();
            }
        };

        TracingManager.Instance.OnPlayerTraced += ResetTracingUI;
        TracingManager.Instance.OnTracingStarted += InitializeTracingUI;
        ResetTracingUI();

        if (TracingManager.Instance.IsTracingActive)
        {
            InitializeTracingUI();
        }
    }

    private void OnDestroy()
    {
        TracingManager.Instance.OnPlayerTraced -= ResetTracingUI;
        TracingManager.Instance.OnTracingStarted -= InitializeTracingUI;
    }

    private void Update()
    {
        if (TracingManager.Instance.IsTracingActive)
        {
            remainingTimeTextField.text = Mathf.Ceil(TracingManager.Instance.GetRealRemainingTracingTime()).ToString() + "s";
            trackingProgress.value = TracingManager.Instance.TracingProgress;
        }
    }

    private void ResetTracingUI()
    {
        remainingTimeTextField.text = "";
        trackingProgress.value = 0;
        inactiveTrackingNote.SetActive(true);
        activeTrackingNote.SetActive(false);
    }

    private void InitializeTracingUI()
    {
        inactiveTrackingNote.SetActive(false);
        activeTrackingNote.SetActive(true);
    }
}