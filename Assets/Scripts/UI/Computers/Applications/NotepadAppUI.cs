using System.Linq;
using System.Text;
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
        StringBuilder multilineFileOutput = new();
        multilineFileOutput.AppendJoin('\n', multilineFileContent);
        if (multilineFileContent.Length > 0) multilineFileOutput.Append('\n');

        if (fileStringSO is FilePasswordStringSO filePasswordStringSO)
        {
            string password = filePasswordStringSO.EncodedCompressedPasswordContent;
            multilineFileOutput.Append(password);
        }

        contentField.ContentFieldClicked += (position) =>
        {
            // MoveCopyMenu(position);
            copyMenuUI.SetCopyPasteMenuEnabled(true);
        };

        contentInputField.text = fileContent = multilineFileOutput.ToString();
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);

        copyMenuUI.InitializeCopyPasteMenuUI(CopyPasteMenuUI.MenuFunction.CopyMenu, contentInputField);
        copyMenuUI.SetCopyPasteMenuEnabled(false);
    }
}