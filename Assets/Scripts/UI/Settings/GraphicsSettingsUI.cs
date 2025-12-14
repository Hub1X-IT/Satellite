using System;
using TMPro;
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

    private void Start()
    {
        graphicsDropdown.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetGraphics);
        resolutionDropdown.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetResolution);
        fullscreenToggle.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetFullscreen);
        vsyncToggle.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetVSync);
        fpsMaxInputField.onSubmit.AddListener((string value) => GraphicsSettingsManager.Instance.SetFPSMax(Int32.Parse(value)));
        fpsDisplayToggle.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetFPSDisplayEnabled);
        headBobToggle.onValueChanged.AddListener(GraphicsSettingsManager.Instance.SetHeadBobEnabled);

        graphicsDropdown.value = GameSettingsManager.Instance.GraphicsIndex;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(GraphicsSettingsManager.Instance.ResolutionDropdownOptions);
        resolutionDropdown.value = GraphicsSettingsManager.Instance.StartResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = GameSettingsManager.Instance.Fullscreen;
        vsyncToggle.isOn = GameSettingsManager.Instance.VSync;
        fpsMaxInputField.text = GameSettingsManager.Instance.FPSMax.ToString();
        fpsDisplayToggle.isOn = GameSettingsManager.Instance.FPSDisplay;

        headBobToggle.isOn = GameSettingsManager.Instance.HeadBobEnabled;
    }
}