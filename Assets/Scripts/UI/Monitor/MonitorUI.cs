using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    // [SerializeField]
    private Monitor monitor;

    private List<TMP_InputField> inputFieldList;

    [SerializeField]
    private ScreenUICursorController monitorCursor;

    [SerializeField]
    private Button testButton, testButton2, testButton3;

    [SerializeField]
    private Canvas notepadCanvas, folderCanvas, doorappCanvas;


    private void Awake()
    {
        // There should be only one object with the script Monitor in the scene!
        monitor = FindAnyObjectByType<Monitor>();
        notepadCanvas.enabled = false;
        folderCanvas.enabled = false;
        doorappCanvas.enabled = false;
        inputFieldList = new();

        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>(true);
        foreach (var inputField in inputFields)
        {
            AddInputField(inputField);
        }

        monitor.MonitorViewSetActive += (enabled) =>
        {
            monitorCursor.SetEnabled(enabled);
        };
        //{testButton.name}: {nameof(testButton.onClick)}
        testButton.onClick.AddListener(() => Debug.Log($"bazinga"));
        testButton.onClick.AddListener(() => NotepadEnable());

        testButton2.onClick.AddListener(() => Debug.Log($"bazinga2"));
        testButton2.onClick.AddListener(() => FolderEnable());

        testButton3.onClick.AddListener(() => Debug.Log($"bazinga3"));
        testButton3.onClick.AddListener(() => DoorAppEnable());

        monitorCursor.enabled = false;
    }

    public void AddInputField(TMP_InputField inputField)
    {
        inputField.onSelect.AddListener(SetCanExitMonitorViewFalse);
        inputField.onDeselect.AddListener(SetCanExitMonitorViewTrue);
        inputFieldList.Add(inputField);
    }

    public void RemoveInputField(TMP_InputField inputField)
    {
        inputField.onSelect.RemoveListener(SetCanExitMonitorViewFalse);
        inputField.onDeselect.RemoveListener(SetCanExitMonitorViewTrue);
        inputFieldList.Remove(inputField);
    }

    private void CloseAll()
    {
        folderCanvas.enabled = false;
        notepadCanvas.enabled = false;
        doorappCanvas.enabled = false;
    }
    private void NotepadEnable()
    {
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
        if (doorappCanvas.enabled == false)
        {
            CloseAll();
            doorappCanvas.enabled = true;
        }
        else
        {
            doorappCanvas.enabled = false;
        }
    }

    private void SetCanExitMonitorViewFalse(string _) => monitor.CanExitMonitorView = false;

    private void SetCanExitMonitorViewTrue(string _) => monitor.CanExitMonitorView = true;
}