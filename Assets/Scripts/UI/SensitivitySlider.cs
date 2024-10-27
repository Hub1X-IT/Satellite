using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private TextMeshProUGUI sliderValueTextField;

    private const float minSliderValue = 0f;
    private const float maxSliderValue = 50f;


    private void Awake()
    {
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }

    private void Start()
    {
        sensitivitySlider.minValue = minSliderValue;
        sensitivitySlider.maxValue = maxSliderValue;
        ChangeSensitivity(GameSettingsManager.MouseSensitivity);
    }

    private void ChangeSensitivity(float value)
    {
        GameSettingsManager.MouseSensitivity = value;
        sliderValueTextField.text = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
    }
}