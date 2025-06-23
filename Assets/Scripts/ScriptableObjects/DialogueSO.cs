using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField]
    private DialogueManager.DialogueSentence[] dialogueSentences;

    public DialogueManager.DialogueSentence[] DialogueSentences => dialogueSentences;

    // May be temporary
    [Tooltip("Game event invoked when the dialogue ends")]
    public GameEventSO DialogueEndedGameEvent;
}