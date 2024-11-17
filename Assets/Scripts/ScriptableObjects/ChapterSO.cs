using UnityEngine;

[CreateAssetMenu]
public class ChapterSO : ScriptableObject
{
    [SerializeField]
    private string title;

    [SerializeField]
    private ObjectiveSO[] objectives;

    public string Title => title;
}
