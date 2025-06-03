using System;
using UnityEngine;
using UnityEngine.Audio;

public static class VolumeController
{
    [Serializable]
    public struct InitializationData
    {
        public AudioMixer MainAudioMixer;
    }

    public enum Volume
    {
        MasterVolume,
        MusicVolume,
        SoundVolume,
        DialogueVolume,
    }

    private static AudioMixer mainAudioMixer;

    public const string MasterVolume = "MasterVolume";
    public const string MusicVolume = "MusicVolume";
    public const string SoundFXVolume = "SoundFXVolume";
    public const string DialogueVolume = "DialogueVolume";

    public static void OnAwake(InitializationData data)
    {
        mainAudioMixer = data.MainAudioMixer;
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
        mainAudioMixer.SetFloat(DialogueVolume, ValueToVolume(GameSettingsManager.DialogueVolume));
    }

    private static float ValueToVolume(float value)
    {
        // Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}