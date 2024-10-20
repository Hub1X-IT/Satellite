using TMPro;
using UnityEngine;

public class CommandPromptUI : MonoBehaviour {


    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text outputField;

    private string inputString;
    private string outputString;


    private void Start() {
        CommandPromptManager.Instance.OnSubmitCommand += CommandPromptManager_OnSubmitCommand;
        // CommandPromptManager.Instance.OnAddCharacter += CommandPromptManager_OnAddCharacter;
        CommandPromptManager.Instance.OnChangeCommand += CommandPromptManager_OnChangeCommand;
    }



    private void Update() {
        inputField.onDeselect.AddListener((string inputString) => inputField.ActivateInputField());               
    }


    private void CommandPromptManager_OnSubmitCommand(object sender, System.EventArgs e) {
        Submit();
    }

    /*
    private void CommandPromptManager_OnAddCharacter(object sender, CommandPromptManager.OnAddCharacterEventArgs e) {
        char character = e.character;
        inputField.text += character;
    }
    */

    private void CommandPromptManager_OnChangeCommand(object sender, CommandPromptManager.OnChangeCommandEventArgs e) {
        string inputFieldText = e.command;
        inputField.text = inputFieldText;
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

    private void AddOutputText(string text) {
        outputString += text;
        outputField.text = outputString;
    }
}
