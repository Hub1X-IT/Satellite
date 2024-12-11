using TMPro;
using UnityEngine;

public class FilePasswordScreenUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName = "Locked file - ";

    [SerializeField]
    private TMP_InputField passwordInputField;

    private string correctPassword;

    private FileSO fileSO;

    public void InitializeFilePasswordScreen(FileSO fileSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();

        this.fileSO = fileSO;

        correctPassword = fileSO.FilePassword;

        monitorApp.SetAppName(BaseAppName + fileSO.SelfName);

        passwordInputField.onEndEdit.AddListener(CheckPassword);
    }

    private void CheckPassword(string password)
    {
        if (password == correctPassword)
        {
            fileSO.IsLocked = false;
            monitorApp.CloseApp();
        }
    }
}