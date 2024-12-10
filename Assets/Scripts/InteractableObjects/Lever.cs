using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; private set; }

    public Transform SelfTransform { get; private set; }

    [SerializeField]
    private Animator leverAnimator;

    [SerializeField]
    private AudioSource leverToggleAudioSource;

    private const string LeverOnTrigger = "LeverOn";
    private const string LeverOffTrigger = "LeverOff";

    private bool isLeverEnabled = true;
    
    //Detection related
    private bool leverOn = true;
    private DetectionManager detectionManager;
    private Desk desk;
    private Server server;

    private void Awake()
    {
        InteractVisual = GetComponent<InteractionVisual>();
        desk = FindAnyObjectByType<Desk>();
        detectionManager = FindAnyObjectByType<DetectionManager>();
        server = FindAnyObjectByType<Server>();
    }

    public void Interact()
    {
        SetLeverEnabled(!isLeverEnabled);
    }

    private void SetLeverEnabled(bool enabled)
    {
        isLeverEnabled = enabled;
        leverAnimator.SetTrigger(enabled ? LeverOnTrigger : LeverOffTrigger);
        leverToggleAudioSource.Play();
        ResetPower();
    }

    private void ResetPower()
    {
        if (leverOn)
        {
            leverOn = false;
            detectionManager.detected = false;
            server.serverTrigger.gameObject.SetActive(false);
            desk.ShouldEnableDeskTrigger = false;
            desk.ToggleDeskTrigger();
        }
        else
        {
            leverOn = true;
            server.serverTrigger.gameObject.SetActive(true);
            desk.ShouldEnableDeskTrigger = true;
            desk.ToggleDeskTrigger();
        }
    }
}
