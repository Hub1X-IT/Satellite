using TMPro;
using UnityEngine;

public class ComputerUIDynamicInputField : MonoBehaviour
{
    private ComputerUI computerUI;

    private TMP_InputField inputField;

    private void OnEnable()
    {
        computerUI = GetComponentInParent<ComputerUI>();
        inputField = GetComponent<TMP_InputField>();
        if (computerUI != null && inputField != null)
        {
            computerUI.AddInputField(inputField);
        }
        else
        {
            Debug.LogWarning($"computerUI or inputField is null: {gameObject.name}");
        }
    }

    private void OnDisable()
    {
        if (computerUI != null && inputField != null)
        {
            computerUI.RemoveInputField(inputField);
        }
        else
        {
            Debug.LogWarning($"computerUI or inputField is null: {gameObject.name}");
        }
    }
}