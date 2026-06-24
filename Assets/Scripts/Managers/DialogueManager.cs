using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField]
    private DialogueRunner dialogueRunner;

    public event Action OnDialogueComplete;

    public bool IsDialogueRunning => dialogueRunner.IsDialogueRunning;

    [SerializeField]
    private DialogueCharacterSO[] dialogueCharacters;

    private Dictionary<string, DialogueCharacterSO> dialogueCharactersDictionary;

    private readonly string[] interactionNotNeededDialogueNodeNames = { "AdditionalLine1", "AdditionalLine2", "AdditionalLine3" };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"Multiple {nameof(DialogueManager)} instances detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        dialogueCharactersDictionary = new();

        foreach (var characterSO in dialogueCharacters)
        {
            string characterId = characterSO.Id;

            if (dialogueCharactersDictionary.ContainsKey(characterId))
            {
                Debug.LogError($"Multiple dialogue characters with ID: {characterId}");
                continue;
            }

            dialogueCharactersDictionary[characterId] = characterSO;
        }

        dialogueRunner.onDialogueComplete.AddListener(() => OnDialogueComplete?.Invoke());
    }

    private void StartDialogue(string nodeName)
    {
        dialogueRunner.StartDialogue(nodeName);
    }

    public void ForceStartDialogue(string nodeName)
    {
        if (IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }

        StartDialogue(nodeName);
    }

    public bool TryStartDialogue(string nodeName)
    {
        if (!IsDialogueRunning)
        {
            StartDialogue(nodeName);
            return true;
        }
        
        return false;
    }

    public DialogueCharacterSO GetDialogueCharacterSO(string characterID)
    {
        return dialogueCharactersDictionary.TryGetValue(characterID, out var characterSO) ? characterSO : null;
    }

    public void StartRandomInteractionNotNeededDialogue()
    {
        int randomIndex = UnityEngine.Random.Range(0, interactionNotNeededDialogueNodeNames.Length);
        TryStartDialogue(interactionNotNeededDialogueNodeNames[randomIndex]);
    }
}
