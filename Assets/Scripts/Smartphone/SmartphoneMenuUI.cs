using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [SerializeField] private EmailUI emailUI;

    [SerializeField] private Button emailButton;

    private bool isEnabled;
    private bool IsEnabled
    {
        get => isEnabled;
        set
        {
            gameObject.SetActive(value);
            isEnabled = value;
        }
    }

    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            emailUI.Show(() => IsEnabled = true);
            IsEnabled = false;
        });
    }
}