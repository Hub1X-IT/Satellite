using TMPro;
using UnityEngine;

public class NewNotepadAppUI : MonitorAppUI
{
    /*
    [SerializeField]
    private TMP_Text contentTextField;
    */

    private const string BaseAppName = "Notepad - ";

    [SerializeField]
    private TMP_InputField contentInputField;

    private FileStringSO fileStringSO;

    private void Awake()
    {
        // contentInputField.onEndEdit.AddListener((str) => Debug.Log(str));
    }

    public void InitializeNotepadAppUI(FileStringSO fileStringSO)
    {
        this.fileStringSO = fileStringSO;
        contentInputField.text = fileStringSO.FileContent;
        SetAppName(BaseAppName + fileStringSO.SelfName);
        contentInputField.onEndEdit.AddListener(fileStringSO.SetFileContent);
    }
}