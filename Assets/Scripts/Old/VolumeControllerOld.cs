using UnityEngine;
using UnityEngine.Audio;

public class VolumeControllerOld : MonoBehaviour
{
    public static VolumeControllerOld Instance { get; private set; }

    [SerializeField] private AudioMixer mainVolumeAudioMixer;
    [SerializeField] private AudioMixer musicVolumeAudioMixer;
    [SerializeField] private AudioMixer soundVolumeAudioMixer;

    private const string VOLUME_PARAMETER_NAME = "MasterVolume";

    private void Awake()
    {
        Instance = this;
    }

    public void SetMainVolume(float value)
    {
        mainVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Main volume: " + value);
    }

    public void SetMusicVolume(float value)
    {
        musicVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Music volume: " + value);
    }

    public void SetSoundVolume(float value)
    {
        soundVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Sound volume: " + value);
    }

    public static float ValueToVolume(float value)
    {
        /// Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}