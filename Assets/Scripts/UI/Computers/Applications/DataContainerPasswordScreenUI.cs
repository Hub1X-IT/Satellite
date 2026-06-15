using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataContainerPasswordScreenUI : MonoBehaviour
{
    public event Action PasswordGuessed;

    private MonitorAppUI monitorApp;

    private const string BaseAppName_Folder = "Locked folder - ";
    private const string BaseAppName_File = "Locked file - ";

    [SerializeField]
    private TMP_InputField passwordInputField;

    [SerializeField]
    private Button submitPasswordButton;

    [SerializeField]
    private EnterableUIObject incorrectPasswordScreen;

    private string correctPassword;

    private DataContainerSO selfDataContainerSO;

    public void InitializeDataContainerPasswordScreen(DataContainerSO dataContainerSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();

        selfDataContainerSO = dataContainerSO;

        correctPassword = dataContainerSO.DataContainerPassword.ToUpper();
        Debug.Log(correctPassword);

        string baseAppName = dataContainerSO is FolderSO _ ? BaseAppName_Folder : BaseAppName_File;
        monitorApp.SetAppName(baseAppName + dataContainerSO.SelfName);

        passwordInputField.onValidateInput += PasswordInputField_OnValidateInput;
        passwordInputField.onSubmit.AddListener(CheckPassword);
        submitPasswordButton.onClick.AddListener(CheckPassword);

        incorrectPasswordScreen.Disable();
    }

    private void OnDestroy()
    {
        PasswordGuessed = null;
    }

    private char PasswordInputField_OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (!char.IsLetter(addedChar))
        {
            return '\0';
        }
        return addedChar;
    }

    private void CheckPassword()
    {
        CheckPassword(null);
    }

    private void CheckPassword(string password = null)
    {
        password ??= passwordInputField.text;
        if (password.ToUpper() == correctPassword)
        {
            selfDataContainerSO.IsLocked = false;
            PasswordGuessed?.Invoke();
            monitorApp.CloseApp();
        }
        else
        {
            incorrectPasswordScreen.Enable(ResetInputField);
        }
    }

    private void ResetInputField()
    {
        // passwordInputField.text = string.Empty;
        passwordInputField.ActivateInputField();
    }
}