using TMPro;
using UnityEngine;

public class NotepadAppUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName = "Notepad - ";

    [SerializeField]
    private TMP_InputField contentInputField;

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

        contentInputField.text = multilineFileOutput;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);
    }
}