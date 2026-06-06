using UnityEngine;

[CreateAssetMenu(fileName = "DialogueCharacterSO", menuName = "DialogueCharacterSO", order = 0)]
public class DialogueCharacterSO : ScriptableObject
{
    public string Id;
    public string DisplayName;

    public Sprite Portrait;
}