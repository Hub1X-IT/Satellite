using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [Header ("UIs")]
    [SerializeField]
    private EmailUI emailUI;
    [SerializeField]
    private ContactsUI contactsUI;

    [Header ("Buttons")]
    [SerializeField]
    private Button emailButton;
    [SerializeField]
    private Button contactsButton;


    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            SetEnabled(false);
            emailUI.Enable(() => SetEnabled(true));
        });
        contactsButton.onClick.AddListener(() =>
        {
            SetEnabled(false);
            contactsUI.Enable(() => SetEnabled(true));
        });
    }
    public void GoToMainMenu()
    {
        // Disable all objects except main menu
        emailUI.Disable();
        contactsUI.Disable();

        SetEnabled(true);
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}