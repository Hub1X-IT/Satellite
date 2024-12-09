using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/ChapterSO")]
public class ChapterSO : ScriptableObject
{
    [SerializeField]
    private string title;

    [SerializeField]
    private ObjectiveSO[] objectives;

    public string Title => title;
}
