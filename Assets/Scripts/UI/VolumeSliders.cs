using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
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

        mainVolumeSlider.onValueChanged.AddListener((value) => ChangeVolume(value, VolumeController.VolumeType.MainVolume));
        musicVolumeSlider.onValueChanged.AddListener((value) => ChangeVolume(value, VolumeController.VolumeType.MusicVolume));
        soundVolumeSlider.onValueChanged.AddListener((value) => ChangeVolume(value, VolumeController.VolumeType.SoundVolume));
    }

    private void Start()
    {
        mainVolumeSlider.value = GameSettingsManager.MainVolume;
        musicVolumeSlider.value = GameSettingsManager.MusicVolume;
        soundVolumeSlider.value = GameSettingsManager.SoundVolume;
    }

    private void ChangeVolume(float value, VolumeController.VolumeType type)
    {
        switch (type)
        {
            case VolumeController.VolumeType.MainVolume:
                GameSettingsManager.MainVolume = VolumeController.ValueToVolume(value);
                break;
            case VolumeController.VolumeType.MusicVolume:
                GameSettingsManager.MusicVolume = VolumeController.ValueToVolume(value);
                break;
            case VolumeController.VolumeType.SoundVolume:
                GameSettingsManager.SoundVolume = VolumeController.ValueToVolume(value);
                break;
        }
        VolumeController.UpdateVolume();
    }
}
