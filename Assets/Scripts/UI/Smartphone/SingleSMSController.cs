using System;
using TMPro;
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

    private bool wasOpened = false;

    [Serializable]
    public class UIElements
    {
        public TMP_Text Name;
        public TMP_Text Time;
        public TMP_Text Title;
        public TMP_Text Description;
    }

    [SerializeField]
    private UIElements elements;

    // Requiring initialization
    private SMSMessage message;
    private Action onFirstOpen;
    private SMSUI smsUI;
    private bool initialized = false;

    private void Awake()
    {
        SmsButton.onClick.AddListener(() =>
        {
            if (!initialized)
            {
                Debug.LogWarning("Message wasn't initialized");
                return;
            }
            if (wasOpened == false)
            {
                SmsButton.image.sprite = readSmsSprite;
                wasOpened = true;
                onFirstOpen?.Invoke();
            }

            smsUI.OpenMessage(message);
        });
    }

    public void Initialize(SMSUI smsUI, SMSMessage message, Action onFirstOpen = null)
    {
        if (initialized)
        {
            Debug.LogWarning("Attempted initializing already initialized message");
            return;
        }

        this.smsUI = smsUI;
        this.message = message;
        this.onFirstOpen = onFirstOpen;
        
        elements.Name.text = message.Sender;
        elements.Time.text = message.Date;
        elements.Title.text = message.Title;
        elements.Description.text = message.Description;

        initialized = true;
    }
}
