using System;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField]
    private DialogueRunner dialogueRunner;

    public event Action OnDialogueComplete;

    public bool IsDialogueRunning => dialogueRunner.IsDialogueRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(DialogueManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        dialogueRunner.onDialogueComplete.AddListener(() => OnDialogueComplete?.Invoke());
    }

    public void StartDialogue(string nodeName)
    {
        dialogueRunner.StartDialogue(nodeName);
    }
}
