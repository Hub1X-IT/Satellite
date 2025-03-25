using System;
using TMPro;
using UnityEngine;

public class DataContainerPasswordScreenUI : MonoBehaviour
{
    public event Action PasswordGuessed;

    private MonitorAppUI monitorApp;

    private const string BaseAppName_Folder = "Locked folder - ";
    private const string BaseAppName_File = "Locked file - ";

    [SerializeField]
    private TMP_InputField passwordInputField;

    [SerializeField]
    private EnterableUIObject incorrectPasswordScreen;

    private string correctPassword;

    private DataContainerSO selfDataContainerSO;

    public void InitializeDataContainerPasswordScreen(DataContainerSO dataContainerSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();

        selfDataContainerSO = dataContainerSO;

        correctPassword = dataContainerSO.DataContainerPassword;
        Debug.Log(correctPassword);

        string baseAppName = dataContainerSO is FolderSO _ ? BaseAppName_Folder : BaseAppName_File;
        monitorApp.SetAppName(baseAppName + dataContainerSO.SelfName);

        passwordInputField.onEndEdit.AddListener(CheckPassword);

        incorrectPasswordScreen.Disable();
    }

    private void OnDestroy()
    {
        PasswordGuessed = null;
    }

    private void CheckPassword(string password)
    {
        if (password == correctPassword)
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
        passwordInputField.text = string.Empty;
    }
}