using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour {


    [SerializeField] private PlayerController playerController;


    [SerializeField] private Slider sensitivitySlider;


    [SerializeField] private TextMeshProUGUI sliderValueTextField;


    [SerializeField] private float maxSliderValue;


    private void Awake() {
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
    }


    private void Start() {
        sensitivitySlider.maxValue = maxSliderValue;
        sensitivitySlider.value = playerController.GetMouseSensitivity();
        SetTextField();
    }


    private void ChangeSensitivity(float sensitivityValue) {
        playerController.SetMouseSensitivity(sensitivityValue);
        SetTextField();
    }


    private void SetTextField() {
        string textFieldContent = (Mathf.Round(sensitivitySlider.value / sensitivitySlider.maxValue * 100)).ToString() + "%";
        sliderValueTextField.text = textFieldContent;
    }
}