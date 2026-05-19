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

        PowerManager.Instance.SetPowerState(enabled);
    }

    private void playSparkParticles()
    {
        sparkParticles.Play();
    }
}
