using TMPro;
using UnityEngine;

public class NotepadAppUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    private string notepadText;


    private void Awake()
    {
        notepadText = "Bazoopa";
        textField.text = notepadText;
    }
}
