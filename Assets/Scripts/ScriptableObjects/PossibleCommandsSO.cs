using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PossibleCommandsSO")]
public class PossibleCommandsSO : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string, GameEventCommandDataSO> possibleCommandsSerializableDictionary;

    public Dictionary<string, GameEventCommandDataSO> PossibleCommandsDictionary
        => possibleCommandsSerializableDictionary.Dictionary;
}