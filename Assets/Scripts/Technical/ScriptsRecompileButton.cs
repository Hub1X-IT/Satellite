using UnityEditor;

public static class ScriptsRecompileButton
{
    [MenuItem("Assets/Recompile scripts")]
    private static void RecompileScripts()
    {
        AssetDatabase.Refresh();
    }
}
