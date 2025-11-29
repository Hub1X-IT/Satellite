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
    }

    private void Start()
    {
        mainVolumeSlider.onValueChanged.AddListener((value) => SetVolume(SoundManager.Volume.MasterVolume, value));
        musicVolumeSlider.onValueChanged.AddListener((value) => SetVolume(SoundManager.Volume.MusicVolume, value));
        soundVolumeSlider.onValueChanged.AddListener((value) => SetVolume(SoundManager.Volume.SoundVolume, value));
        dialogueVolumeSlider.onValueChanged.AddListener((value) => SetVolume(SoundManager.Volume.DialogueVolume, value));

        mainVolumeSlider.value = GameSettingsManager.Instance.MainVolume;
        musicVolumeSlider.value = GameSettingsManager.Instance.MusicVolume;
        soundVolumeSlider.value = GameSettingsManager.Instance.SoundVolume;
        dialogueVolumeSlider.value = GameSettingsManager.Instance.DialogueVolume;
    }

    private void SetVolume(SoundManager.Volume volumeType, float value)
    {
        GameSettingsManager.Instance.SetVolume(volumeType, value);
        SoundManager.Instance.UpdateVolume();
    }
}
