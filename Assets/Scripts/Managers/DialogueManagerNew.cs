using System;
using UnityEngine;

public class DialogueManagerNew : MonoBehaviour
{
    [Serializable]
    public class DialogueSentence
    {
        public DialogueCharacterSO Character;
        [TextArea(3, 10)]
        public string Sentence;
        public AudioClip SentenceAudioClip;
        public float MinSentenceTime;
    }
    
    [Serializable]
    private class DialogueInvokeData
    {
        public GameEventSO[] GameEvents;
        public DialogueSO DialogueSO;
    }

    public static event Action<DialogueSentence> NewDialogueSentenceStarted;
    public static event Action DialogueEnded;
    public static event Action<bool> OnCanStartNewSentence;

    [SerializeField]
    private DialogueInvokeData[] dialoguesInvokeData;

    private DialogueSO currentDialogueSO;
    private int currentSentenceIndex;
    private int currentDialogueLength;

    private bool canGoToNextSentence;
    private bool isNextSentenceTimerActive;
    private float nextSentenceTimer;

    private void Awake()
    {
        GameInput.OnNextDialogueSentenceAction += OnNextDialogueSentence;

        foreach (var dialogueInvokeData in dialoguesInvokeData)
        {
            foreach (var gameEvent in dialogueInvokeData.GameEvents)
            {
                gameEvent.EventRaised += () => StartNewDialogue(dialogueInvokeData.DialogueSO);
            }
        }
    }

    private void Update()
    {
        if (isNextSentenceTimerActive)
        {
            if (nextSentenceTimer <= 0)
            {
                canGoToNextSentence = true;
                OnCanStartNewSentence?.Invoke(true);
                isNextSentenceTimerActive = false;
            }
            else
            {
                nextSentenceTimer -= Time.deltaTime;
            }
        }
    }

    private void OnDestroy()
    {
        NewDialogueSentenceStarted = null;
        DialogueEnded = null;
        OnCanStartNewSentence = null;
    }

    private void StartNewDialogue(DialogueSO dialogueSO)
    {
        currentDialogueSO = dialogueSO;

        currentSentenceIndex = 0;
        currentDialogueLength = dialogueSO.DialogueSentences.Length;

        StartNewDialogueSentence(dialogueSO.DialogueSentences[0]);

        GameInput.PlayerInputActions.Dialogue.Enable();
    }

    private void OnNextDialogueSentence()
    {
        if (currentDialogueSO != null)
        {
            if (canGoToNextSentence)
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
        if (currentDialogueSO != null)
        {
            currentDialogueSO.DialogueEndedGameEvent.TryRaiseEvent();
        }
    }

    private void StartNewDialogueSentence(DialogueSentence sentence)
    {
        NewDialogueSentenceStarted?.Invoke(sentence);

        canGoToNextSentence = false;
        isNextSentenceTimerActive = true;
        nextSentenceTimer = sentence.MinSentenceTime;
        OnCanStartNewSentence?.Invoke(false);
    }
}
