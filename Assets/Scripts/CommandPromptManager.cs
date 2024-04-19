using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPromptManager : MonoBehaviour {


    public static CommandPromptManager Instance { get; private set; }


    public event EventHandler OnSubmitCommand;
    public event EventHandler<OnAddCharacterEventArgs> OnAddCharacter;
    public class OnAddCharacterEventArgs : EventArgs {
        public char character;
    }
    

    private string command;


    private void Awake() {
        Instance = this;
    }


    private void Start() {
        GameInput.Instance.OnKeyboardInputAction += GameInput_OnKeyboardInputAction;
    }

    private void GameInput_OnKeyboardInputAction(object sender, GameInput.OnKeyboardInputActionEventArgs e) {
        AddCharacter(e.key);
    }

    private void AddCharacter(char character) {
        command += character;
        OnAddCharacter?.Invoke(this, new OnAddCharacterEventArgs {
            character = character
        });
    }

    public void RunCommand() {
        
    }
}