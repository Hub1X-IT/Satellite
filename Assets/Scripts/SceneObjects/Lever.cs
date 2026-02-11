using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Interactable interactionTrigger;

    [SerializeField]
    private Animator leverAnimator;
    
    [SerializeField]
    private AudioSource leverToggleAudioSource;

    private const string IsLeverOnParam = "IsLeverOn";

    private bool isLeverEnabled;

    private void Start()
    {
        interactionTrigger.OnInteractionTriggered += () => SetLeverEnabled(!isLeverEnabled);
        SetLeverEnabled(PowerManager.Instance.IsPowerOn);
    }

    private void SetLeverEnabled(bool enabled)
    {
        isLeverEnabled = enabled;

        leverAnimator.SetBool(IsLeverOnParam, enabled);
        leverToggleAudioSource.Play();

        PowerManager.Instance.SetPowerState(enabled);
    }
}
