using System;
using UnityEngine;
using UnityEngine.Audio;

public static class VolumeController
{
    [Serializable]
    public struct InitializationData
    {
        public AudioMixer mainVolumeAudioMixer;
        public AudioMixer musicVolumeAudioMixer;
        public AudioMixer soundVolumeAudioMixer;
    }


    public enum VolumeType
    {
        MainVolume,
        MusicVolume,
        SoundVolume,
    }


    private static AudioMixer mainVolumeAudioMixer;
    private static AudioMixer musicVolumeAudioMixer;
    private static AudioMixer soundVolumeAudioMixer;


    private const string VOLUME_PARAMETER_NAME = "MasterVolume";


    public static void InitializeOnAwake(InitializationData data)
    {
        mainVolumeAudioMixer = data.mainVolumeAudioMixer;
        musicVolumeAudioMixer = data.musicVolumeAudioMixer;
        soundVolumeAudioMixer = data.soundVolumeAudioMixer;
    }


    public static void InitializeOnStart()
    {
        UpdateVolume();
    }


    public static void UpdateVolume()
    {
        mainVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, GameSettingsManager.MainVolume);
        musicVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, GameSettingsManager.MusicVolume);
        soundVolumeAudioMixer.SetFloat(VOLUME_PARAMETER_NAME, GameSettingsManager.SoundVolume);
    }


    public static float ValueToVolume(float value)
    {
        /// Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}