using TMPro;
using UnityEngine;

public class ComputerUIInputField : MonoBehaviour
{
    private ComputerUI computerUI;

    private TMP_InputField inputField;

    private void Awake()
    {
        computerUI = GetComponentInParent<ComputerUI>();
        inputField = GetComponent<TMP_InputField>();
        computerUI.AddInputField(inputField);
    }

    private void OnDestroy()
    {
        computerUI.RemoveInputField(inputField);
    }
}