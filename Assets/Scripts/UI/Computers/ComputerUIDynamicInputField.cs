using System;
using TMPro;
using UnityEngine;

public class ComputerUIDynamicInputField : MonoBehaviour
{
    public event Action<ComputerUIDynamicInputField> InputFieldSelected;
    public event Action InputFieldDeselected;

    private ComputerUI computerUI;

    private TMP_InputField inputField;

    private void OnEnable()
    {
        computerUI = GetComponentInParent<ComputerUI>();
        inputField = GetComponent<TMP_InputField>();
        if (computerUI != null)
        {
            computerUI.AddInputField(this);
            // Debug.Log($"{computerUI} - add input field: {name}");
        }
        else
        {
            Debug.LogWarning($"computerUI or inputField is null: {gameObject.name}");
        }

        if (inputField != null)
        {
            inputField.onSelect.AddListener(OnInputFieldSelected);
            inputField.onDeselect.AddListener(OnInputFieldDeselected);
        }
    }

    private void OnDisable()
    {
        if (computerUI != null)
        {
            computerUI.RemoveInputField(this);
            // Debug.Log($"{computerUI} - remove input field: {name}");
        }
        else
        {
            Debug.LogWarning($"computerUI or inputField is null: {gameObject.name}");
        }

        if (inputField != null)
        {
            inputField.onSelect.RemoveListener(OnInputFieldSelected);
            inputField.onDeselect.RemoveListener(OnInputFieldDeselected);
        }
    }

    private void OnInputFieldSelected(string _)
    {
        InputFieldSelected?.Invoke(this);
    }

    private void OnInputFieldDeselected(string _)
    {
        InputFieldDeselected?.Invoke();
    }
}