using UnityEngine;
using System.Collections.Generic;

public class Lever : MonoBehaviour, IInteractable
{
    public InteractionVisual InteractVisual { get; private set; }

    public Transform SelfTransform { get; private set; }

    [SerializeField]
    private Animator leverAnimator;

    [SerializeField]
    private MeshRenderer cubeObject;

    [SerializeField]
    private Material leverOffMaterial;

    [SerializeField]
    private Material leverOnMaterial;

    [SerializeField]
    private AudioSource leverToggleAudioSource;

    private const string LeverOnTrigger = "LeverOn";
    private const string LeverOffTrigger = "LeverOff";

    private bool isLeverEnabled = false;

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
        if (enabled)
        {
            leverAnimator.SetTrigger(LeverOnTrigger);
            cubeObject.material = leverOnMaterial;
        }
        else
        {
            leverAnimator.SetTrigger(LeverOffTrigger);
            cubeObject.material = leverOffMaterial;
        }
        leverToggleAudioSource.Play();
    }
}
