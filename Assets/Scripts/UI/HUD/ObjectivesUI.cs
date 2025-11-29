using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text chapterTextField;

    [SerializeField]
    private TMP_Text objectiveTextField;

    private void Start()
    {
        ObjectivesManager.Instance.OnChapterChanged += SetChapter;
        ObjectivesManager.Instance.OnObjectiveChanged += SetObjective;
    }

    public void SetChapter(string chapter)
    {
        chapterTextField.text = chapter;
    }

    private void SetObjective(string objective)
    {
        objectiveTextField.text = objective;
    }
}