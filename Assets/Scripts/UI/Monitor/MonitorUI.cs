using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    // [SerializeField]
    private Monitor monitor;

    private CanvasGroup monitorCanvasGroup;

    private List<TMP_InputField> inputFieldList;
    
    [SerializeField]
    private ScreenUICursorController monitorCursor;

    [SerializeField]
    private Button notepadButton;
    [SerializeField]
    private Button folderButton;
    [SerializeField]
    private Button doorAppButton;

    [SerializeField]
    private Canvas notepadCanvas;
    [SerializeField]
    private Canvas folderCanvas;
    [SerializeField]
    private Canvas doorAppCanvas;

    private bool notepadEnabled;
    private bool folderEnabled;
    private bool doorAppEnabled;


    private void Awake()
    {
        // There should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();

        monitorCanvasGroup = GetComponent<CanvasGroup>();

        inputFieldList = new();

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            AddInputField(inputField);
        }

        monitor.MonitorViewSetActive += (enabled) =>
        {
            monitorCursor.SetEnabled(enabled);
            monitorCanvasGroup.blocksRaycasts = enabled;
        };

        notepadButton.onClick.AddListener(NotepadEnable);
        folderButton.onClick.AddListener(FolderEnable);
        doorAppButton.onClick.AddListener(DoorAppEnable);

        /*
        notepadButton.onClick.AddListener(() => SetNotepadEnabled(!notepadEnabled));
        folderButton.onClick.AddListener(() => SetFolderEnabled(!folderEnabled));
        doorAppButton.onClick.AddListener(() => SetDoorAppEnabled(!doorAppEnabled));

        SetNotepadEnabled(false);
        SetFolderEnabled(false);
        SetDoorAppEnabled(false);
        */

        notepadCanvas.enabled = false;
        folderCanvas.enabled = false;
        doorAppCanvas.enabled = false;

        monitorCursor.enabled = false;
    }


    private void AddInputField(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(SetCanExitMonitorViewFalse);
        inputField.onDeselect.AddListener(SetCanExitMonitorViewTrue);
        inputFieldList.Add(inputField);
    }

    private void RemoveInputField(TMP_InputField inputField)
    {
        inputField.onSelect.RemoveListener(SetCanExitMonitorViewFalse);
        inputField.onDeselect.RemoveListener(SetCanExitMonitorViewTrue);
        inputFieldList.Remove(inputField);
    }


    private void CloseAll()
    {
        folderCanvas.enabled = false;
        notepadCanvas.enabled = false;
        doorAppCanvas.enabled = false;
    }


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

    private void FolderEnable()
    {
        Debug.Log("bazinga2");

        if (folderCanvas.enabled == false)
        {
            CloseAll();
            folderCanvas.enabled = true;
        }
        else
        {
            folderCanvas.enabled = false;
        }
    }

    private void DoorAppEnable()
    {
        Debug.Log("bazinga3");

        if (doorAppCanvas.enabled == false)
        {
            CloseAll();
            doorAppCanvas.enabled = true;
        }
        else
        {
            doorAppCanvas.enabled = false;
        }
    }


    private void SetNotepadEnabled(bool enabled)
    {
        CloseAll();
        notepadCanvas.enabled = enabled;
        notepadEnabled = enabled;
    }

    private void SetFolderEnabled(bool enabled)
    {
        CloseAll();
        folderCanvas.enabled = enabled;
        folderEnabled = enabled;
    }

    private void SetDoorAppEnabled(bool enabled)
    {
        CloseAll();
        doorAppCanvas.enabled = enabled;
        doorAppEnabled = enabled;
    }


    private void SetCanExitMonitorViewFalse(string _) => monitor.CanExitMonitorView = false;

    private void SetCanExitMonitorViewTrue(string _) => monitor.CanExitMonitorView = true;
}