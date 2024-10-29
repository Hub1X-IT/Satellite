using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [SerializeField]
    private EmailUI emailUI;

    [SerializeField]
    private Button emailButton;

    public bool IsSmartphoneMenuEnabled { get; private set; }


    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            emailUI.Show(() => SetSmartphoneMenuEnabled(true));
            SetSmartphoneMenuEnabled(false);
        });
    }

    private void SetSmartphoneMenuEnabled(bool enabled)
    {
        IsSmartphoneMenuEnabled = enabled;
        gameObject.SetActive(enabled);
    }
}