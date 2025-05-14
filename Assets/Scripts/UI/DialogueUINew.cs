using TMPro;
using UnityEngine;

public class DialogueUINew : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueUI;

    [SerializeField]
    private TMP_Text dialogueNameTextField;

    [SerializeField]
    private TMP_Text dialogueSentenceTextField;

    private void Awake()
    {
        DialogueManagerNew.NewDialogueSentenceStarted += ShowDialogue;
        DialogueManagerNew.DialogueEnded += HideDialogue;
    }

    private void ShowDialogue(DialogueManagerNew.DialogueSentence dialogueStatement)
    {
        dialogueNameTextField.text = dialogueStatement.SayerName;
        dialogueSentenceTextField.text = dialogueStatement.Sentence;
        dialogueUI.SetActive(true);
    }

    private void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }
}