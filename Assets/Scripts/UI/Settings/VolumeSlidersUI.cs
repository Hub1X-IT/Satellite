using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersUI : MonoBehaviour
{
    private const float minSliderValue = 0.0001f;
    private const float maxSliderValue = 1f;

    [SerializeField]
    private Slider mainVolumeSlider;

    [SerializeField]
    private Slider musicVolumeSlider;

    [SerializeField]
    private Slider soundVolumeSlider;


    private void Awake()
    {
        mainVolumeSlider.minValue = minSliderValue;
        musicVolumeSlider.minValue = minSliderValue;
        soundVolumeSlider.minValue = minSliderValue;

        mainVolumeSlider.maxValue = maxSliderValue;
        musicVolumeSlider.maxValue = maxSliderValue;
        soundVolumeSlider.maxValue = maxSliderValue;

        mainVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.MasterVolume, value));
        musicVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.MusicVolume, value));
        soundVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.SoundVolume, value));
    }

    private void Start()
    {
        mainVolumeSlider.value = GameSettingsManager.MainVolume;
        musicVolumeSlider.value = GameSettingsManager.MusicVolume;
        soundVolumeSlider.value = GameSettingsManager.SoundVolume;
    }

    private void SetVolume(VolumeController.Volume volumeType, float value)
    {
        GameSettingsManager.SetVolume(volumeType, value);
        VolumeController.UpdateVolume();
    }
}
