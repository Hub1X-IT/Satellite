using TMPro;
using UnityEngine;

public class DataContainerPasswordScreenUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName_Folder = "Locked folder - ";
    private const string BaseAppName_File = "Locked file - ";

    [SerializeField]
    private TMP_InputField passwordInputField;

    private string correctPassword;

    private DataContainerSO selfDataContainerSO;

    public void InitializeDataContainerPasswordScreen(DataContainerSO dataContainerSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();

        selfDataContainerSO = dataContainerSO;

        correctPassword = dataContainerSO.DataContainerPassword;

        string baseAppName = dataContainerSO is FolderSO _ ? BaseAppName_Folder : BaseAppName_File;
        monitorApp.SetAppName(baseAppName + dataContainerSO.SelfName);

        passwordInputField.onEndEdit.AddListener(CheckPassword);
    }

    private void CheckPassword(string password)
    {
        if (password == correctPassword)
        {
            selfDataContainerSO.IsLocked = false;
            monitorApp.CloseApp();
        }
    }
}