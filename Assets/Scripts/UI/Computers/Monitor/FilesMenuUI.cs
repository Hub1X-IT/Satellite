using UnityEngine;
using UnityEngine.UI;

public class FilesMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button notepadAppButton;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private NotepadAppUI notepadApp;

    private bool isNotepadAppEnabled;

    private void Awake()
    {
        notepadAppButton.onClick.AddListener(() => SetNotepadEnabled(!isNotepadAppEnabled));
        closeButton.onClick.AddListener(() => CloseAll());
        SetNotepadEnabled(false);
    }

    private void CloseAll()
    {
        notepadApp.gameObject.SetActive(false);
        isNotepadAppEnabled = false;
    }

    private void SetNotepadEnabled(bool enabled)
    {
        CloseAll();
        notepadApp.gameObject.SetActive(enabled);
        isNotepadAppEnabled = enabled;
    }

    /*
    private void NotepadEnable()
    {
        Debug.Log("bazinga");


        if (notepadCanvas.enabled == false)
        {
            CloseAll();
            notepadCanvas.enabled = true;
        }
        else
        {
            notepadCanvas.enabled = false;
        }

    }
    */
}
