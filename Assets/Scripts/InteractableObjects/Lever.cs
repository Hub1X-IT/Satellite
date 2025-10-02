using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; private set; }

    public Transform SelfTransform { get; private set; }

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
    

    private void Awake()
    {
        InteractVisual = GetComponent<InteractionVisual>();
    }

    public void Interact()
    {
        SetLeverEnabled(!isLeverEnabled);
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

        DetectionManager.SetServerPowerEnabled(enabled);
    }
}
