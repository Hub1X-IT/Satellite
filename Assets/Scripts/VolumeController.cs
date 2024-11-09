using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Audio;

public static class VolumeController
{
    [Serializable]
    public struct InitializationData
    {
        public AudioMixer mainAudioMixer;
    }

    public enum Volume
    {
        MasterVolume,
        MusicVolume,
        SoundVolume,
    }

    private static AudioMixer mainAudioMixer;

    public const string MasterVolume = "MasterVolume";
    public const string MusicVolume = "MusicVolume";
    public const string SoundFXVolume = "SoundFXVolume";

    public static void OnAwake(InitializationData data)
    {
        mainAudioMixer = data.mainAudioMixer;
    }

    public static void OnStart()
    {
        UpdateVolume();
    }

    public static void UpdateVolume()
    {
        mainAudioMixer.SetFloat(MasterVolume, ValueToVolume(GameSettingsManager.MainVolume));
        mainAudioMixer.SetFloat(MusicVolume, ValueToVolume(GameSettingsManager.MusicVolume));
        mainAudioMixer.SetFloat(SoundFXVolume, ValueToVolume(GameSettingsManager.SoundVolume));
    }

    private static float ValueToVolume(float value)
    {
        // Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}