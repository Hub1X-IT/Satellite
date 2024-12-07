using TMPro;
using UnityEngine;

public class NotepadAppUI : MonitorAppUI
{
    private const string BaseAppName = "Notepad - ";

    [SerializeField]
    private TMP_InputField contentInputField;

    public void InitializeNotepadAppUI(FileStringSO fileStringSO)
    {
        contentInputField.text = fileStringSO.DisplayedFileContent;
        SetAppName(BaseAppName + fileStringSO.SelfName);
    }
}