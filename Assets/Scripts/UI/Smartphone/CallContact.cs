using UnityEngine;
using UnityEngine.UI;

public class CallContact : MonoBehaviour
{
    [SerializeField]
    private ContactSO contact;

    [SerializeField]
    private PhonecallManager phonecallManager;

    [SerializeField]
    private Button callButton;
    
    private void Start()
    {
        callButton.onClick.AddListener(() => phonecallManager.CallNPC(contact.ContactName));
    }
}
