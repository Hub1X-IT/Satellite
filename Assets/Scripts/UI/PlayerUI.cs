using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    
    [SerializeField] private TMP_Text chapter;
    [SerializeField] private TMP_Text objectiveDescription;


    void Awake() {
        SetObjective("Day 1", "Take the phone from night table");
    }


    public void SetObjective(string title, string description) {
        chapter.text = title;
        objectiveDescription.text = description;

        // chapter.text = title.Replace(chapter.text, title);
        // objectiveDescription.text = description.Replace(objectiveDescription.text, description);
    }
}