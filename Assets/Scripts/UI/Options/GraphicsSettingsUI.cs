using TMPro;
using UnityEngine;

public class GraphicSettingsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown graphicsDropdown;

    private void Awake()
    {
        graphicsDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetGraphics);

        resolutionDropdown.onValueChanged.AddListener(GraphicsSettingsManager.SetResolution);
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