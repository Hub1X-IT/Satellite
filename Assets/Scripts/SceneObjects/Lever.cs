using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Interactable interactionTrigger;

    [SerializeField]
    private Animator leverAnimator;

    [SerializeField]
    private AudioSource leverToggleAudioSource;
    
    [SerializeField]
    private ParticleSystem sparkParticles;

    private const string IsLeverOnParam = "IsLeverOn";

    private bool isLeverEnabled;

    private void Start()
    {
        interactionTrigger.OnInteractionTriggered += () => SetLeverEnabled(!isLeverEnabled);

        isLeverEnabled = PowerManager.Instance.IsPowerOn;
        leverAnimator.SetBool(IsLeverOnParam, isLeverEnabled);
    }

    private void SetLeverEnabled(bool enabled)
    {
        isLeverEnabled = enabled;

        leverAnimator.SetBool(IsLeverOnParam, enabled);
        leverToggleAudioSource.Play();
    }

    private void SetLightEnabled() //used in Lever Animator as an Event
    {
        PowerManager.Instance.SetPowerState(!PowerManager.Instance.IsPowerOn);
    }

    private void PlaySparkParticles() //used in Lever Animator as an Event
    {
        sparkParticles.Play();
    }
}
