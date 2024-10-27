using UnityEngine;
using UnityEngine.Audio;

public class VolumeControllerNew {

    private static AudioMixer MainVolumeAudioMixer { get; set; }
    private static AudioMixer MusicVolumeAudioMixer { get; set; }
    private static AudioMixer SoundVolumeAudioMixer { get; set; }

    private const string VOLUME_PARAMETER_NAME = "MasterVolume";

    public static void InitializeOnAwake(AudioMixer mainVolumeAudioMixer, AudioMixer musicVolumeAudioMixer, AudioMixer soundVolumeAudioMixer)  {
        MainVolumeAudioMixer = mainVolumeAudioMixer;
        MusicVolumeAudioMixer = musicVolumeAudioMixer;
        SoundVolumeAudioMixer = soundVolumeAudioMixer;
    }

    public static void SetMainVolume(float value) {
        MainVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Main volume: " + value);
    }

    public static void SetMusicVolume(float value) {
        MusicVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Music volume: " + value);
    }

    public static void SetSoundVolume(float value) {
        SoundVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, value);
        Debug.Log("Sound volume: " + value);
    }

    public static float ValueToVolume(float value) {
        /// Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}