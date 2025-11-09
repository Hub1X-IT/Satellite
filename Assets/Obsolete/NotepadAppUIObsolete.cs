using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadAppUIObsolete : MonoBehaviour
{
    [SerializeField]
    private FilesMenuUIObsolete filesMenu;

    [SerializeField]
    private NotepadTextSO textInput;

    [SerializeField]
    private TMP_Text textField;

    private void Awake()
    {
        textField.text = textInput.textboxText;
    }
}
