using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConvertedPasswordUI : MonoBehaviour
{
    public event Action<ConvertedPasswordUI> GoBackToPasswordTriggered;

    [SerializeField]
    private Button goBackToPasswordButton;

    [SerializeField]
    private TMP_Text convertedPasswordTextField;

    private string convertedPassword;

    public string ConvertedPassword => convertedPassword;

    public void InitializeConvertedPasswordUI(string passwordToDisplay)
    {
        goBackToPasswordButton.onClick.AddListener(() => GoBackToPasswordTriggered?.Invoke(this));
        convertedPassword = convertedPasswordTextField.text = passwordToDisplay;
        gameObject.SetActive(true);
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}