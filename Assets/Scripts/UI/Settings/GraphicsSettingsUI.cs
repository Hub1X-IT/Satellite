using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettingsUI : MonoBehaviour
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
    private TMP_Text fpsDisplay;

    private void Awake()
    {
        graphicsDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetGraphics);

        resolutionDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetResolution);

        fullscreenToggle.onValueChanged.AddListener(GraphicsSettingsManager.SetFullscreen);

        vsyncToggle.onValueChanged.AddListener(GraphicsSettingsManager.SetVSync);

        fpsMaxInputField.onSubmit.AddListener((string value) => GraphicsSettingsManager.SetFPSMax(Int32.Parse(value)));

        fpsDisplayToggle.onValueChanged.AddListener((bool enabled) =>
        {
            fpsDisplay.gameObject.SetActive(enabled);
            GameSettingsManager.SetFPSDisplay(enabled);
        });
    }

    private void Start()
    {
        graphicsDropdown.value = GameSettingsManager.GraphicsIndex;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(GraphicsSettingsManager.ResolutionDropdownOptions);
        resolutionDropdown.value = GameSettingsManager.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}