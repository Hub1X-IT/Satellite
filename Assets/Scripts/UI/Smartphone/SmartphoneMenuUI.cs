using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [Header ("UIs")]
    [SerializeField]
    private SMSUI smsUI;
    [SerializeField]
    private ContactsOverviewUI contactsUI;

    [Header ("Buttons")]
    [SerializeField]
    private Button smsButton;
    [SerializeField]
    private Button contactsButton;


    private void Awake()
    {
        smsButton.onClick.AddListener(() =>
        {
            //SetEnabled(false);
            smsUI.Enable(() => SetEnabled(true));
        });
        contactsButton.onClick.AddListener(() =>
        {
            //SetEnabled(false);
            contactsUI.Enable(() => SetEnabled(true));
        });
    }
    public void GoToMainMenu()
    {
        // Disable all objects except main menu
        smsUI.Disable();
        contactsUI.Disable();

        SetEnabled(true);
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}