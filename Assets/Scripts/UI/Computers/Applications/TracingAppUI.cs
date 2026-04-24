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
    private Image[] tracingDots;

    [SerializeField]
    private Color connectionActiveDotColor;
    [SerializeField]
    private Color notTracedDotColor;
    [SerializeField]
    private Color tracedDotColor;

    private int currentTracedDotCount;

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
            remainingTimeTextField.text = Mathf.Ceil(TracingManager.Instance.TracingTimer).ToString() + "s";

            int tracedDotCount = (int)Mathf.Round(TracingManager.Instance.TracingProgress * tracingDots.Length);
            if (tracedDotCount > currentTracedDotCount)
            {
                tracingDots[^tracedDotCount].color = tracedDotColor;
                currentTracedDotCount = tracedDotCount;
            }
        }
    }

    private void ResetTracingUI()
    {
        remainingTimeTextField.text = "";

        foreach (var tracingDot in tracingDots)
        {
            tracingDot.color = connectionActiveDotColor;
        }
    }

    private void InitializeTracingUI()
    {
        foreach (var tracingDot in tracingDots)
        {
            tracingDot.color = notTracedDotColor;
        }
        
        currentTracedDotCount = (int)Mathf.Round(TracingManager.Instance.TracingProgress * tracingDots.Length);
        for (int i = tracingDots.Length - currentTracedDotCount; i < tracingDots.Length; i++)
        {
            tracingDots[i].color = tracedDotColor;
        }
    }
}