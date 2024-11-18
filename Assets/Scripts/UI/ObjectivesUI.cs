using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text chapterTextField;

    [SerializeField]
    private TMP_Text objectiveTextField;

    [SerializeField]
    private string defaultChapter;

    [SerializeField]
    private string defaultObjective;

    private void Awake()
    {
        SetChapter(defaultChapter);
        SetObjective(defaultObjective);
    }

    public void SetChapter(string chapter)
    {
        chapterTextField.text = chapter;
    }

    public void SetChapter(ChapterSO chapter)
    {
        chapterTextField.text = chapter.Title;
    }

    public void SetObjective(string objective)
    {
        objectiveTextField.text = objective;
    }

    public void SetObjective(ObjectiveSO objective)
    {
        objectiveTextField.text = objective.Objective;
    }
}