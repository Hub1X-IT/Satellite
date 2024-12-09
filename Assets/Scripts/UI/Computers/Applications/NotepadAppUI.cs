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

        contentInputField.text = fileStringSO.FileContent;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);
    }
}