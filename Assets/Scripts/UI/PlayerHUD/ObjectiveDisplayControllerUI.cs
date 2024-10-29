using TMPro;
using UnityEngine;

public class ObjectiveDisplayControllerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text chapter;

    [SerializeField]
    private TMP_Text objectiveDescription;


    private void Awake()
    {
        SetObjective("Day 1", "Take the phone from night table");
    }


    public void SetObjective(string title, string description)
    {
        chapter.text = title;
        objectiveDescription.text = description;
    }
}