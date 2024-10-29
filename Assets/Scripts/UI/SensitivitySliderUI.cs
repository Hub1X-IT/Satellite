using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderUI : MonoBehaviour
{
    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private TextMeshProUGUI sliderValueTextField;

    private const float minSliderValue = 0f;
    private const float maxSliderValue = 50f;


    private void Awake()
    {
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        sensitivitySlider.minValue = minSliderValue;
        sensitivitySlider.maxValue = maxSliderValue;
    }

    private void Start()
    {
        SetSensitivity(GameSettingsManager.MouseSensitivity);
    }

    private void SetSensitivity(float value)
    {
        GameSettingsManager.SetMouseSensitivity(value);
        sliderValueTextField.text = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
    }
}