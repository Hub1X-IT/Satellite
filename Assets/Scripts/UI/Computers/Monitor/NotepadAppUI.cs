using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadAppUI : MonoBehaviour
{
    [SerializeField]
    private FilesMenuUI filesMenu;

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private NotepadTextSO textInput;

    [SerializeField]
    private TMP_Text textField;

    private void Awake()
    {
        textField.text = textInput.textboxText;
        closeButton.onClick.AddListener(() => filesMenu.gameObject.SetActive(false));
    }
}
