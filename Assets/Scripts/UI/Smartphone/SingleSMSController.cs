using UnityEngine;
using UnityEngine.UI;

public class SingleSMSController : MonoBehaviour
{
    [SerializeField]
    private Button SmsButton;

    [SerializeField]
    private RectTransform SmsRectTransform;

    [SerializeField]
    private Sprite readSmsSprite;

    [SerializeField]
    private EnterableUIObject SmsContent;

    [SerializeField]
    private GameEventSO objectiveEvent;

    private bool wasOpened = false;

    private void Awake()
    {
        SmsButton.onClick.AddListener(() =>
        {
            if(objectiveEvent != null)
            {
                objectiveEvent.TryRaiseEvent();
                objectiveEvent = null;
            }
            DisableSmsObject();
            if (wasOpened == false)
            {
                SmsButton.image.sprite = readSmsSprite;
                wasOpened = true;
            }
            SmsContent.Enable(EnableSmsObject);
        });
    }

    private void EnableSmsObject()
    {
        SmsRectTransform.gameObject.SetActive(true);
    }
    private void DisableSmsObject()
    {
        SmsRectTransform.gameObject.SetActive(false);
    }
}
