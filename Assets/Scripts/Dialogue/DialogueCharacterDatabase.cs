using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacterDatabase : MonoBehaviour
{
    [SerializeField]
    private DialogueCharacterSO[] dialogueCharacters;

    private Dictionary<string, DialogueCharacterSO> dialogueCharactersDictionary;

    private void Awake()
    {
        dialogueCharactersDictionary = new();

        foreach(var characterSO in dialogueCharacters)
        {
            string characterId = characterSO.Id;

            if (dialogueCharactersDictionary.ContainsKey(characterId))
            {
                Debug.LogError($"Multiple dialogue characters with ID: {characterId}");
                continue;
            }

            dialogueCharactersDictionary[characterId] = characterSO;
        }
    }

    public DialogueCharacterSO GetDialogueCharacterSO(string characterID)
    {
        return dialogueCharactersDictionary.TryGetValue(characterID, out var characterSO) ? characterSO : null;
    }
}