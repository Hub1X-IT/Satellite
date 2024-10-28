using System;
using UnityEngine;

public static class CommandPromptManager
{
    private static readonly char[] excludedCharacters =
    {
        '\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A',
        '\u000B', '\u000C', '\u000D', '\u000E', '\u000F', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015',
        '\u0016', '\u0017', '\u0018', '\u0019', '\u001A', '\u001B', '\u001C', '\u001D', '\u001E', '\u001F', '\u007F'
    };


    private static readonly char backspaceCode = '\u0008';


    public static event Action OnSubmitCommand;


    public static event Action<string> OnChangeCommand;


    private static string command;


    public static void InitializeOnStart()
    {
        GameInput.OnKeyboardInputAction += GameInput_OnKeyboardInputAction;

        GameInput.OnSubmitAction += GameInput_OnSubmitAction;
    }


    private static void GameInput_OnKeyboardInputAction(char character)
    {
        AddCharacter(character);
    }


    private static void GameInput_OnSubmitAction()
    {
        SubmitCommand();
    }


    private static void AddCharacter(char character)
    {
        if (character == backspaceCode)
        {
            RemoveCharacter();
            return;
        }

        if (CheckIfCanAdd(character))
        {
            command += character;
        }
        CommandChanged();
    }

    private static bool CheckIfCanAdd(char character)
    {
        foreach (char c in excludedCharacters)
        {
            if (character == c)
            {
                int i = c;
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! debug log !!!!!!!!!!!!
                // Debug.Log("False, " + i);
                return false;
            }
        }
        return true;
    }

    private static void RemoveCharacter()
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! debug log !!!!!!!!!!!!
        // Debug.Log("CommandPromptManager: RemoveCharacter()");
        if (command.Length > 0)
        {
            Debug.Log(command.Length);
            command = command.Remove(command.Length - 1);
        }
        CommandChanged();
    }

    private static void CommandChanged()
    {
        OnChangeCommand?.Invoke(command);

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! debug log !!!!!!!!!!!!
        // Debug.Log(command);
    }

    private static void SubmitCommand()
    {
        OnSubmitCommand?.Invoke();
        RunCommand();
    }

    public static void RunCommand()
    {
        command = null;
    }
}