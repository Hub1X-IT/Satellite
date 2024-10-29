using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderUI : MonoBehaviour
{
    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private TextMeshProUGUI sliderValueTextField;

    [SerializeField]
    private float minSliderValue = 0.1f;
    [SerializeField]
    private float maxSliderValue = 50f;


    private void Awake()
    {
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        sensitivitySlider.minValue = minSliderValue;
        sensitivitySlider.maxValue = maxSliderValue;
    }

    private void Start()
    {
        SetSensitivity(GameSettingsManager.MouseSensitivity);
        sensitivitySlider.value = GameSettingsManager.MouseSensitivity;
    }

    private void SetSensitivity(float value)
    {
        GameSettingsManager.SetMouseSensitivity(value);
        sliderValueTextField.text = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
    }
}