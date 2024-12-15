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

        /*
        string[] multilineFileContent = fileStringSO.MultilineFileContent;
        string multilineFileOutput = "";
        foreach (var line in multilineFileContent)
        {
            multilineFileOutput += line + '\n';
        }
        */

        string outputText = fileStringSO.ShouldCompressContent ? 
            TextCompressor.GetCompressedText(fileStringSO.FileContent) : fileStringSO.FileContent;

        contentInputField.text = outputText;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);
    }
}