public static class VirtualClipboard
{
    private static string currentlyCopiedString;

    public static void InitializeVirtualClipboard()
    {
        currentlyCopiedString = string.Empty;
    }

    public static void SetClipboardText(string text)
    {
        currentlyCopiedString = text;
    }

    public static string GetClipboardText()
    {
        return currentlyCopiedString;
    }
}