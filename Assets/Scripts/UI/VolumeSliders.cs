using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

    private const float minSliderValue = 0.0001f;
    private const float maxSliderValue = 1f;

    [SerializeField][Tooltip("Value between 0.0001 and 1")] private float defaultSliderValue = 0.5f;

    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;


    private void Awake() {
        mainVolumeSlider.minValue = minSliderValue; mainVolumeSlider.maxValue = maxSliderValue; mainVolumeSlider.value = defaultSliderValue;
        musicVolumeSlider.minValue = minSliderValue; musicVolumeSlider.maxValue = maxSliderValue; musicVolumeSlider.value = defaultSliderValue;
        soundVolumeSlider.minValue = minSliderValue; soundVolumeSlider.maxValue = maxSliderValue; soundVolumeSlider.value = defaultSliderValue;

        mainVolumeSlider.onValueChanged.AddListener(ChangeMainVolume);
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(ChangeSoundVolume);

        ChangeMainVolume(defaultSliderValue); ChangeMusicVolume(defaultSliderValue); ChangeSoundVolume(defaultSliderValue);
    }

    private void ChangeMainVolume(float value) { VolumeController.Instance.SetMainVolume(VolumeController.ValueToVolume(value)); }
    private void ChangeMusicVolume(float value) { VolumeController.Instance.SetMainVolume(VolumeController.ValueToVolume(value)); }
    private void ChangeSoundVolume(float value) { VolumeController.Instance.SetMainVolume(VolumeController.ValueToVolume(value)); }
}
