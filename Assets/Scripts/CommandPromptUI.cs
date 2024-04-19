using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour {


    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text outputField;

    private string inputString;
    private string outputString;


    private void Update() {
        inputField.onDeselect.AddListener((string inputString) => inputField.ActivateInputField());

        if (Input.GetKeyDown(KeyCode.Return)) {
            Submit();
        }
    }


    private void Submit() {
        inputString = inputField.text;
        ChangeOutputText();
        inputField.text = null;
        inputField.ActivateInputField();
    }

    private void ChangeOutputText() {
        if (inputString.Length > 0) {
            outputString += "\n>>> " + inputString;
            outputField.text = outputString;
        }
    }
}
