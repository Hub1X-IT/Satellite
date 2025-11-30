using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Interactable interactionTrigger;

    [SerializeField]
    private Animator leverAnimator;

    [SerializeField]
    private GameObject lightSource;

    [SerializeField]
    private Material lightbulbMaterial;

    [SerializeField]
    private AudioSource leverToggleAudioSource;

    private const string LeverOnTrigger = "LeverOn";
    private const string LeverOffTrigger = "LeverOff";

    private bool isLeverEnabled = true;


    private void Start()
    {
        interactionTrigger.OnInteractionTriggered += () => SetLeverEnabled(!isLeverEnabled);
    }

    private void SetLeverEnabled(bool enabled)
    {
        isLeverEnabled = enabled;
        leverAnimator.SetTrigger(enabled ? LeverOnTrigger : LeverOffTrigger);
        lightSource.SetActive(enabled);
        if (enabled)
        {
            lightbulbMaterial.EnableKeyword("_EMISSION");
        }
        else
        {
            lightbulbMaterial.DisableKeyword("_EMISSION");
        }
        leverToggleAudioSource.Play();

        DetectionManager.Instance.SetServerPowerEnabled(enabled);
    }
}
