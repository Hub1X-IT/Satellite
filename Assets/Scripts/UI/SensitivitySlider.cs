using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour {

    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private TextMeshProUGUI sliderValueTextField;

    private float minSliderValue = 0f;
    private float maxSliderValue = 50f;   


    private void Awake() {
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }


    private void Start() {
        sensitivitySlider.maxValue = maxSliderValue;
        sensitivitySlider.minValue = minSliderValue;
        sensitivitySlider.value = GameSettings.MouseSensitivity;
        SetTextField();
    }


    private void ChangeSensitivity(float sensitivityValue) {
        GameSettings.SetMouseSensitivity(sensitivityValue);
        SetTextField();
    }


    private void SetTextField() {
        string textFieldContent = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
        sliderValueTextField.text = textFieldContent;
    }
}