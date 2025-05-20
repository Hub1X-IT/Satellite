using System;
using UnityEngine;

public class DialogueManagerNew : MonoBehaviour
{
    [Serializable]
    public class DialogueSentence
    {
        public string SayerName;
        public string Sentence;
    }
    
    [Serializable]
    private class DialogueInvokeData
    {
        public GameEventSO[] GameEvents;
        public DialogueSO DialogueSO;
    }

    public static event Action<DialogueSentence> NewDialogueSentenceStarted;
    public static event Action DialogueEnded;

    [SerializeField]
    private DialogueInvokeData[] dialoguesInvokeData;

    private DialogueSO currentDialogueSO;
    private int currentSentenceIndex;
    private int currentDialogueLength;

    private void Awake()
    {
        GameInput.OnNextDialogueSentenceAction += OnNextDialogueSentence;

        foreach (var dialogueInvokeData in dialoguesInvokeData)
        {
            foreach (var gameEvent in dialogueInvokeData.GameEvents)
            {
                StartNewDialogue(dialogueInvokeData.DialogueSO);
            }
        }
    }

    private void OnDestroy()
    {
        NewDialogueSentenceStarted = null;
        DialogueEnded = null;
    }

    private void StartNewDialogue(DialogueSO dialogueSO)
    {
        currentDialogueSO = dialogueSO;

        currentSentenceIndex = 0;
        currentDialogueLength = dialogueSO.DialogueSenteces.Length;

        StartNewDialogueSentence(dialogueSO.DialogueSenteces[0]);

        GameInput.PlayerInputActions.Dialogue.Enable();
    }

    private void OnNextDialogueSentence()
    {
        if (currentDialogueSO != null)
        {
            currentSentenceIndex++;
            if (currentSentenceIndex >= currentDialogueLength)
            {
                EndDialogue();
            }
            else
            {
                StartNewDialogueSentence(currentDialogueSO.DialogueSenteces[currentSentenceIndex]);
            }
        }
        else
        {
            Debug.LogWarning("Next dialogue sentence requested when no dialogue active");
        }
    }

    private void EndDialogue()
    {
        GameInput.PlayerInputActions.Dialogue.Disable();
        DialogueEnded?.Invoke();
    }

    private void StartNewDialogueSentence(DialogueSentence dialogue)
    {
        NewDialogueSentenceStarted?.Invoke(dialogue);
    }

}
