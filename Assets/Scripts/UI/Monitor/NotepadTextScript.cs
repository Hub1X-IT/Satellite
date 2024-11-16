using UnityEngine;

[CreateAssetMenu(fileName = "NotepadTextScript", menuName = "Scriptable Objects/NotepadTextScript")]
public class NotepadTextScript : ScriptableObject
{
    [Tooltip("Text for the notepad")]
    public string textboxtext;
}
