using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadAppUI : MonoBehaviour
{
    private MonitorAppUI monitorApp;

    private const string BaseAppName = "Notepad - ";

    private string fileContent;

    private TMP_InputField contentInputField;

    [SerializeField]
    private NotepadAppContentFieldUI contentField;

    [SerializeField]
    private RectTransform copyMenu;

    [SerializeField]
    private NotepaddAppCopyButtonUI notepaddAppCopyButton;

    private bool canDisableCopyMenu;

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
            SetCopyMenuEnabled(true);
        };

        contentInputField.onDeselect.AddListener((_) =>
        {
            if (canDisableCopyMenu)
            {
                SetCopyMenuEnabled(false);
            }
        });
        notepaddAppCopyButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            CopyText();
            SetCopyMenuEnabled(false);
        });

        notepaddAppCopyButton.OnMouseOverButton += (isOverButton) =>
        {
            canDisableCopyMenu = !isOverButton;
        };

        contentInputField.text = fileContent = multilineFileOutput;
        monitorApp.SetAppName(BaseAppName + fileStringSO.SelfName);

        SetCopyMenuEnabled(false);
        canDisableCopyMenu = true;
    }

    private void SetCopyMenuEnabled(bool enabled)
    {
        copyMenu.gameObject.SetActive(enabled);
    }

    private void MoveCopyMenu(Vector2 newPosition)
    {
        copyMenu.anchoredPosition = newPosition;
    }

    private void CopyText()
    {
        VirtualClipboard.SetClipboardText(fileContent);
    }
}