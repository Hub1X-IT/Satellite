using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUINew : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueUI;

    [SerializeField]
    private Image characterImage;

    [SerializeField]
    private TMP_Text dialogueNameTextField;

    [SerializeField]
    private TMP_Text dialogueSentenceTextField;

    private void Awake()
    {
        DialogueManagerNew.NewDialogueSentenceStarted += ShowDialogue;
        DialogueManagerNew.DialogueEnded += HideDialogue;
        HideDialogue();
    }

    private void ShowDialogue(DialogueManagerNew.DialogueSentence dialogueStatement)
    {
        characterImage.sprite = dialogueStatement.Character.CharacterImage;
        dialogueNameTextField.text = dialogueStatement.Character.CharacterName;
        dialogueSentenceTextField.text = dialogueStatement.Sentence;
        dialogueUI.SetActive(true);
    }

    private void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }
}