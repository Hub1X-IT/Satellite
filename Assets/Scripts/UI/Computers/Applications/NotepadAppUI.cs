using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadAppUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName = "Notepad - ";

    private string fileContent;

    [SerializeField]
    private TMP_InputField contentInputField;

    [SerializeField]
    private Button copyButton;

    public void InitializeNotepadAppUI(FileStringSO fileStringSO)
    {
        monitorApp = GetComponent<MonitorAppUI>();
        
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

        copyButton.onClick.AddListener(CopyText);

        contentInputField.text = fileContent = multilineFileOutput;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);
    }

    private void CopyText()
    {
        VirtualClipboard.SetClipboardText(fileContent);
    }
}