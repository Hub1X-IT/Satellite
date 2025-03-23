using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/GameEvent<CommandData>", order = 1)]
public class GameEventCommandDataSO : GameEventSO<CommandData>
{
    [SerializeField]
    private int requiredArgumentsNumber;

    public int RequiredArgumentsNumber => requiredArgumentsNumber;
}