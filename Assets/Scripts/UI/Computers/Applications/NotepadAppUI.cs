using TMPro;
using UnityEngine;

public class NotepadAppUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName = "Notepad - ";

    private string fileContent;

    private TMP_InputField contentInputField;

    [SerializeField]
    private NotepadAppContentFieldUI contentField;

    [SerializeField]
    private CopyPasteMenuUI copyMenuUI;

    public void InitializeNotepadAppUI(FileStringSO fileStringSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();
        contentInputField = contentField.GetComponent<TMP_InputField>();

        string[] multilineFileContent = fileStringSO.MultilineFileContent;
        string multilineFileOutput = "";
        foreach (var line in multilineFileContent)
        {
            multilineFileOutput += line + '\n';
        }

        if (fileStringSO is FilePasswordStringSO filePasswordStringSO)
        {
            string password = filePasswordStringSO.EncodedCompressedPasswordContent;
            multilineFileOutput += password;
        }

        contentField.ContentFieldClicked += (position) =>
        {
            // MoveCopyMenu(position);
            copyMenuUI.SetCopyPasteMenuEnabled(true);
        };

        contentInputField.text = fileContent = multilineFileOutput;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);

        copyMenuUI.InitializeCopyPasteMenuUI(CopyPasteMenuUI.MenuFunction.CopyMenu, contentInputField);
        copyMenuUI.SetCopyPasteMenuEnabled(false);
    }
}