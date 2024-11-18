using TMPro;
using UnityEngine;

public class NotepadAppUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    NotepadTextSO textinput;

    private void Awake()
    {
        textField.text = textinput.textboxtext;
    }
}
