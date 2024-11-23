using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadAppUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    [SerializeField]
    private FilesMenuUI filesMenu;

    [SerializeField]
    NotepadTextSO textinput;

    [SerializeField]
    Button closebutton;

    private void Awake()
    {
        textField.text = textinput.textboxtext;
        closebutton.onClick.AddListener(() => filesMenu.gameObject.SetActive(false));
    }
}
