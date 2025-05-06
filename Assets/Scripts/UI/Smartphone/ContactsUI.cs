using System;
using UnityEngine;
using UnityEngine.UI;

public class ContactsUI : MonoBehaviour
{
    /*
    [SerializeField]
    private Button emailExitButton;
    */

    private Action contactsOverviewClosed;


    private void Awake()
    {
        /*
        emailExitButton.onClick.AddListener(() =>
        {
            emailOverviewClosed();
            Disable();
        });
        */
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
}