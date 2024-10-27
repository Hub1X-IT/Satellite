using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

    private const float minSliderValue = 0.0001f;
    private const float maxSliderValue = 1f;

    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;


    private void Awake() {
        mainVolumeSlider.minValue = minSliderValue; mainVolumeSlider.maxValue = maxSliderValue;
        musicVolumeSlider.minValue = minSliderValue; musicVolumeSlider.maxValue = maxSliderValue;
        soundVolumeSlider.minValue = minSliderValue; soundVolumeSlider.maxValue = maxSliderValue;

        mainVolumeSlider.onValueChanged.AddListener(ChangeMainVolume);
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    private void Start() {
        ChangeMainVolume(GameSettingsManager.MainVolume);
        ChangeMusicVolume(GameSettingsManager.MusicVolume);
        ChangeSoundVolume(GameSettingsManager.SoundVolume);

        mainVolumeSlider.value = GameSettingsManager.MainVolume;
        musicVolumeSlider.value = GameSettingsManager.MusicVolume;
        soundVolumeSlider.value = GameSettingsManager.SoundVolume;
    }

    private void ChangeMainVolume(float value) {
        VolumeController.Instance.SetMainVolume(VolumeController.ValueToVolume(value));
        GameSettingsManager.SetMainVolume(value);
    }

    private void ChangeMusicVolume(float value) {
        VolumeController.Instance.SetMusicVolume(VolumeController.ValueToVolume(value));
        GameSettingsManager.SetMusicVolume(value);
    }

    private void ChangeSoundVolume(float value) {
        VolumeController.Instance.SetSoundVolume(VolumeController.ValueToVolume(value));
        GameSettingsManager.SetSoundVolume(value);
    }
}
