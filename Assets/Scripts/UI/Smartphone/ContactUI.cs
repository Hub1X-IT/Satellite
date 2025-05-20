using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContactUI : MonoBehaviour
{
    private ContactSO thisContactSO;

    [SerializeField]
    private Button callButton;

    [SerializeField]
    private TMP_Text contactNameTextField;

    public void InitializeContactUI(ContactSO contactSO)
    {
        thisContactSO = contactSO;
        contactNameTextField.text = contactSO.ContactName;
        callButton.onClick.AddListener(CallContact);
    }

    private void CallContact()
    {
        PhonecallManager.StartOutcomingCall(thisContactSO);
    }
}