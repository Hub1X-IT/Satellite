using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Serializable]
    public class DialogueSentence
    {
        public DialogueCharacterSO Character;
        [TextArea(3, 10)]
        public string Sentence;
        public AudioClip SentenceAudioClip;
        public float SentenceTime;
    }

    [Serializable]
    private class DialogueInvokeData
    {
        public GameEventSO[] GameEvents;
        public DialogueSO DialogueSO;
    }

    [SerializeField]
    private bool debugDialogueSkipping;

    public static event Action<DialogueSentence> NewDialogueSentenceStarted;
    public static event Action DialogueEnded;

    [SerializeField]
    private DialogueInvokeData[] dialoguesInvokeData;

    private DialogueSO currentDialogueSO;
    private int currentSentenceIndex;
    private int currentDialogueLength;

    private bool isSentenceTimeTimerActive;
    private float sentenceTimeTimer;

    private void Awake()
    {
        GameInput.OnNextDialogueSentenceAction += StartNextDialogueSentence;

        foreach (var dialogueInvokeData in dialoguesInvokeData)
        {
            foreach (var gameEvent in dialogueInvokeData.GameEvents)
            {
                gameEvent.EventRaised += () => StartNewDialogue(dialogueInvokeData.DialogueSO);
            }
        }

        if (debugDialogueSkipping)
        {
            Debug.LogWarning("Debug dialogue skipping is active.");
        }
    }

    private void Update()
    {
        if (isSentenceTimeTimerActive)
        {
            if (sentenceTimeTimer <= 0)
            {
                isSentenceTimeTimerActive = false;
                StartNextDialogueSentence();
            }
            else
            {
                sentenceTimeTimer -= Time.deltaTime;
            }
        }
    }

    private void OnDestroy()
    {
        NewDialogueSentenceStarted = null;
        DialogueEnded = null;
        ResetGameEvents();
    }

    private void ResetGameEvents()
    {
        foreach (var dialogueInvokeData in dialoguesInvokeData)
        {
            foreach (var gameEvent in dialogueInvokeData.GameEvents)
            {
                gameEvent.ResetGameEvent();
            }
        }
    }

    private void StartNewDialogue(DialogueSO dialogueSO)
    {
        currentDialogueSO = dialogueSO;

        currentSentenceIndex = 0;
        currentDialogueLength = dialogueSO.DialogueSentences.Length;

        StartNewDialogueSentence(dialogueSO.DialogueSentences[0]);

        GameInput.PlayerInputActions.Dialogue.Enable();
    }

    private void StartNextDialogueSentence()
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
                StartNewDialogueSentence(currentDialogueSO.DialogueSentences[currentSentenceIndex]);
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
        if (currentDialogueSO != null && currentDialogueSO.DialogueEndedGameEvent != null)
        {
            currentDialogueSO.DialogueEndedGameEvent.TryRaiseEvent();
        }
    }

    private void StartNewDialogueSentence(DialogueSentence sentence)
    {
        NewDialogueSentenceStarted?.Invoke(sentence);

        bool shouldActivateSentenceTimeTimer = sentence.SentenceTime > 0;
        isSentenceTimeTimerActive = shouldActivateSentenceTimeTimer;
        sentenceTimeTimer = sentence.SentenceTime;
    }
}
