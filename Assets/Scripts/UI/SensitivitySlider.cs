using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour {

    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private TextMeshProUGUI sliderValueTextField;

    [SerializeField] private float minSliderValue = 0f;
    [SerializeField] private float maxSliderValue = 50f;


    private void Awake() {
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }

    private void Start() {
        sensitivitySlider.maxValue = maxSliderValue;
        sensitivitySlider.minValue = minSliderValue;
        sensitivitySlider.value = GameSettingsManager.MouseSensitivity;
        SetTextField();
    }

    private void ChangeSensitivity(float sensitivityValue) {
        GameSettingsManager.SetMouseSensitivity(sensitivityValue);
        SetTextField();
    }

    private void SetTextField() {
        sliderValueTextField.text = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
    }
}