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

    [SerializeField]
    private Slider dialogueVolumeSlider;


    private void Awake()
    {
        mainVolumeSlider.minValue = minSliderValue;
        musicVolumeSlider.minValue = minSliderValue;
        soundVolumeSlider.minValue = minSliderValue;
        dialogueVolumeSlider.minValue = minSliderValue;

        mainVolumeSlider.maxValue = maxSliderValue;
        musicVolumeSlider.maxValue = maxSliderValue;
        soundVolumeSlider.maxValue = maxSliderValue;
        dialogueVolumeSlider.maxValue = maxSliderValue;

        mainVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.MasterVolume, value));
        musicVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.MusicVolume, value));
        soundVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.SoundVolume, value));
        dialogueVolumeSlider.onValueChanged.AddListener((value) => SetVolume(VolumeController.Volume.DialogueVolume, value));
    }

    private void Start()
    {
        mainVolumeSlider.value = GameSettingsManager.MainVolume;
        musicVolumeSlider.value = GameSettingsManager.MusicVolume;
        soundVolumeSlider.value = GameSettingsManager.SoundVolume;
        dialogueVolumeSlider.value = GameSettingsManager.DialogueVolume;
    }

    private void SetVolume(VolumeController.Volume volumeType, float value)
    {
        GameSettingsManager.SetVolume(volumeType, value);
        VolumeController.UpdateVolume();
    }
}
