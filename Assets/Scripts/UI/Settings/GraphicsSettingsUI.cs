using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown graphicsDropdown;

    [SerializeField]
    private Toggle fullscreenToggle;

    [SerializeField]
    private Toggle vsyncToggle;

    [SerializeField]
    private TMP_InputField fpsMaxInputField;

    [SerializeField]
    private Toggle fpsDisplayToggle;

    [SerializeField]
    private Toggle headBobToggle;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlin headBob;

    public event Action<bool> OnFPSDisplayToggled;

    private void Awake()
    {
        graphicsDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetGraphics);

        resolutionDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetResolution);

        fullscreenToggle.onValueChanged.AddListener(GraphicsSettingsManager.SetFullscreen);

        vsyncToggle.onValueChanged.AddListener(GraphicsSettingsManager.SetVSync);

        fpsMaxInputField.onSubmit.AddListener((string value) => GraphicsSettingsManager.SetFPSMax(Int32.Parse(value)));

        fpsDisplayToggle.onValueChanged.AddListener((bool enabled) =>
        {
            OnFPSDisplayToggled?.Invoke(enabled);
            GameSettingsManager.SetFPSDisplay(enabled);
        });

        headBobToggle.onValueChanged.AddListener((bool enabled) =>
        {
            if (headBob != null)
            {
                headBob.AmplitudeGain = enabled ? 0.6f : 0f;
            }
            GameSettingsManager.SetHeadBobEnabled(enabled);
        });
    }

    private void Start()
    {
        graphicsDropdown.value = GameSettingsManager.GraphicsIndex;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(GraphicsSettingsManager.ResolutionDropdownOptions);
        resolutionDropdown.value = GameSettingsManager.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = GameSettingsManager.Fullscreen;
        vsyncToggle.isOn = GameSettingsManager.VSync;
        fpsMaxInputField.text = GameSettingsManager.FPSMax.ToString();
        fpsDisplayToggle.isOn = GameSettingsManager.FPSDisplay;

        headBobToggle.isOn = GameSettingsManager.HeadBobEnabled;
    }
}