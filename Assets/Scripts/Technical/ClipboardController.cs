using UnityEngine;

public static class ClipboardController
{
    public static void SetClipboardText(string text)
    {
        GUIUtility.systemCopyBuffer = text;
    }

    public static string GetClipboardText()
    {
        return GUIUtility.systemCopyBuffer;
    }
}