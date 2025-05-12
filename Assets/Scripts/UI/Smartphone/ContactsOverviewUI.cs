using System;
using UnityEngine;
using UnityEngine.UI;

public class ContactsOverviewUI : MonoBehaviour
{
    private Action contactsOverviewClosed;

    /*
    [SerializeField]
    private Button contactsExitButton;
    */

    [SerializeField]
    private ContactUI contactUIPrefab;

    [SerializeField]
    private Transform contactListHolder;

    private void Awake()
    {
        /*
        contactsExitButton.onClick.AddListener(() =>
        {
            contactsOverviewClosed();
            Disable();
        });
        */

        RefreshContactsList();
    }

    public void Enable(Action onCloseAction)
    {
        contactsOverviewClosed = onCloseAction;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void RefreshContactsList()
    {
        foreach (var contactSO in PhonecallManager.ContactList)
        {
            ContactUI contactUI = Instantiate(contactUIPrefab.gameObject, contactListHolder).GetComponent<ContactUI>();
            contactUI.InitializeContactUI(contactSO);
        }
    }
}