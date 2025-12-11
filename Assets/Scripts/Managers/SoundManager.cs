using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Volume
    {
        MasterVolume,
        MusicVolume,
        SoundVolume,
        DialogueVolume,
    }

    [SerializeField]
    private AudioMixer mainAudioMixer;

    [SerializeField]
    private AudioSource[] pausePersistentSounds;

    [SerializeField]
    private bool unpauseOnLoad;

    public const string MasterVolume = "MasterVolume";
    public const string MusicVolume = "MusicVolume";
    public const string SoundFXVolume = "SoundFXVolume";
    public const string DialogueVolume = "DialogueVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(SoundManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateVolume();

        foreach (var sound in pausePersistentSounds)
        {
            sound.ignoreListenerPause = true;
        }

        if (unpauseOnLoad)
        {
            AudioListener.pause = false;
        }

        if (GameManager.Instance)
        {
            GameManager.Instance.GamePausedUnpaused += (paused) =>
            {
                AudioListener.pause = paused;
            };
        }
    }

    public void UpdateVolume()
    {
        mainAudioMixer.SetFloat(MasterVolume, ValueToVolume(GameSettingsManager.Instance.MainVolume));
        mainAudioMixer.SetFloat(MusicVolume, ValueToVolume(GameSettingsManager.Instance.MusicVolume));
        mainAudioMixer.SetFloat(SoundFXVolume, ValueToVolume(GameSettingsManager.Instance.SoundVolume));
        mainAudioMixer.SetFloat(DialogueVolume, ValueToVolume(GameSettingsManager.Instance.DialogueVolume));
    }

    private static float ValueToVolume(float value)
    {
        // Value has to be between 0.0001 and 1 to work properly.
        return Mathf.Log10(value) * 20;
    }
}