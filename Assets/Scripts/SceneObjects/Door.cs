using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private InteractionTrigger doorTrigger;

    [SerializeField]
    private Animator doorAnimator;

    [SerializeField]
    private AudioSource doorAudioSource;

    [SerializeField]
    private bool isInverted = false;

    [SerializeField][Tooltip("True: door open\nFalse: door closed")]
    private bool defaultState = false;

    private const string IS_INVERTED_BOOL = "IsInverted";
    private const string DOOR_OPEN_BOOL = "DoorOpen";
    private string DOOR_ANIM_STATE;

    private bool isDoorOpen;


    private void Awake()
    {
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);

        doorTrigger.InteractVisual = GetComponent<InteractionVisual>();

        doorTrigger.InteractionTriggered += () => SetDoorOpen(!isDoorOpen, true);
    }

    private void OnEnable()
    {
        SetDoorOpen(defaultState, false);
    }

    private void SetDoorOpen(bool open, bool shouldPlaySound)
    {
        isDoorOpen = open;
        doorAnimator.SetBool(IS_INVERTED_BOOL, isInverted);
        doorAnimator.SetBool(DOOR_OPEN_BOOL, isDoorOpen); 
        if (open && shouldPlaySound)
        {
            doorAudioSource.Play();
        }
        else if (!open && shouldPlaySound)
        {
            
        }
    }
}
