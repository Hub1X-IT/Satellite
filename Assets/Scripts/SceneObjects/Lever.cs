using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private InteractionTrigger leverTrigger;

    [SerializeField]
    private Animator leverAnimator;

    private const string SET_LEVER_ON = "LeverOn";
    private const string SET_LEVER_OFF = "LeverOff";

    private bool isLeverOn = false;

    private void Awake()
    {
        leverTrigger.InteractVisual = GetComponent<InteractionVisual>();
    }

    private void Start()
    {
        leverTrigger.InteractionTriggered += () => SetLeverOn();
    }
    
    private void SetLeverOn()
    {
        if (isLeverOn)
        {
            leverAnimator.SetTrigger(SET_LEVER_ON);
            isLeverOn = true;
        }
        else
        {
            leverAnimator.SetTrigger(SET_LEVER_OFF);
            isLeverOn = false;
        }
    }
}
