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
        public float MaxSentenceTime;
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
    public static event Action<bool> OnCanStartNewSentence;

    [SerializeField]
    private DialogueInvokeData[] dialoguesInvokeData;

    private DialogueSO currentDialogueSO;
    private int currentSentenceIndex;
    private int currentDialogueLength;

    private bool canGoToNextSentence;
    private bool isMinSentenceTimeTimerActive;
    private float minSentenceTimeTimer;

    private bool isMaxSentenceTimeTimerActive;
    private float maxSentenceTimeTimer;

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
        if (isMinSentenceTimeTimerActive)
        {
            if (minSentenceTimeTimer <= 0)
            {
                canGoToNextSentence = true;
                OnCanStartNewSentence?.Invoke(true);
                isMinSentenceTimeTimerActive = false;
            }
            else
            {
                minSentenceTimeTimer -= Time.deltaTime;
            }
        }
        if (isMaxSentenceTimeTimerActive)
        {
            if (maxSentenceTimeTimer <= 0)
            {
                StartNextDialogueSentence();
                isMaxSentenceTimeTimerActive = false;
            }
            else
            {
                maxSentenceTimeTimer -= Time.deltaTime;
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

    private void StartNextDialogueSentence()
    {
        if (currentDialogueSO != null)
        {
            if (canGoToNextSentence || debugDialogueSkipping)
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
        if (currentDialogueSO != null && currentDialogueSO.DialogueEndedGameEvent != null)
        {
            currentDialogueSO.DialogueEndedGameEvent.TryRaiseEvent();
        }
    }

    private void StartNewDialogueSentence(DialogueSentence sentence)
    {
        NewDialogueSentenceStarted?.Invoke(sentence);

        bool shouldActivateMinSentenceTimeTimer = sentence.MinSentenceTime > 0;
        canGoToNextSentence = !shouldActivateMinSentenceTimeTimer;
        isMinSentenceTimeTimerActive = shouldActivateMinSentenceTimeTimer;
        minSentenceTimeTimer = sentence.MinSentenceTime;
        OnCanStartNewSentence?.Invoke(!shouldActivateMinSentenceTimeTimer);

        bool shouldActivateMaxSentenceTimeTimer = sentence.MaxSentenceTime > 0;
        isMaxSentenceTimeTimerActive = shouldActivateMaxSentenceTimeTimer;
        maxSentenceTimeTimer = sentence.MaxSentenceTime;
    }
}
