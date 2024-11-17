using System.Collections.Generic;
using UnityEngine;

public static class CommandPromptManager
{
    private static string currentCommand;

    private static Queue<string> previousCommandsQueue = new();

    public static void SubmitCommand(string command)
    {
        previousCommandsQueue.Enqueue(command);
        Debug.Log(command);
    }
}
