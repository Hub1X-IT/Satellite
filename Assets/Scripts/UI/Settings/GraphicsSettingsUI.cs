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

    private void Awake()
    {
        graphicsDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetGraphics);

        resolutionDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetResolution);

        fullscreenToggle.onValueChanged.AddListener(GraphicsSettingsManager.SetFullscreen);
    }

    private void Start()
    {
        graphicsDropdown.value = GameSettingsManager.GraphicsIndex;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(GraphicsSettingsManager.ResolutionOptionsStrings);
        resolutionDropdown.value = GameSettingsManager.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}