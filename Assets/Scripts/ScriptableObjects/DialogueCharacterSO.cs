using UnityEngine;

[CreateAssetMenu(menuName = "DialogueCharacterSO")]
public class DialogueCharacterSO : ScriptableObject
{
    [SerializeField]
    private Sprite characterImage;
    [SerializeField]
    private string characterName;

    public Sprite CharacterImage => characterImage;
    public string CharacterName => characterName;
}