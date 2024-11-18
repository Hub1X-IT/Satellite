using UnityEngine;

[CreateAssetMenu(menuName = "NotepadText")]
public class NotepadTextSO : ScriptableObject
{
    [Tooltip("Text for the notepad")]
    public string textboxtext;
}
