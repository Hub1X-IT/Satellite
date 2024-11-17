using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text chapterTextField;

    [SerializeField]
    private TMP_Text objectiveTextField;

    private void Awake()
    {
        SetChapter("Day 1");
        SetObjective("Take the phone from the night table");
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