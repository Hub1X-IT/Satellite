using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text objectiveTitle;
    public TMP_Text objectiveDescription;

    void Awake()
    {
        SetObjective("Day 1", "Take the phone from night table");
    }

    public void SetObjective(string title, string description)
    {
        objectiveTitle.text = title.Replace(objectiveTitle.text, title);
        objectiveDescription.text = description.Replace(objectiveDescription.text, description);
    }
    void Update()
    {
        
    }
}
