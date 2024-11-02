using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [SerializeField]
    private EmailUI emailUI;

    [SerializeField]
    private Button emailButton;


    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            SetEnabled(false);
            emailUI.Enable(() => SetEnabled(true));
        });
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}