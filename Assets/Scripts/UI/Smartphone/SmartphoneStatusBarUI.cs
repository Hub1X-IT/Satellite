using System;
using UnityEngine;
using TMPro;

public class SmartphoneStatusBarUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currentTimeTextField;

    private int lastMinute;

    private void Start()
    {
        UpdateTime();
    }

    private void Update()
    {
        if (DateTime.Now.Minute != lastMinute)
        {
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        lastMinute = DateTime.Now.Minute;
        currentTimeTextField.text = GetFormattedTime(DateTime.Now.Hour, DateTime.Now.Minute);
    }

    private string GetFormattedTime(int hour, int minute)
    {
        bool am = hour < 12;
        hour = hour % 12 == 0 ? 12 : hour % 12;

        string strMinute = minute.ToString().PadLeft(2, '0');

        string outputString = $"{hour}:{strMinute} {(am ? "AM" : "PM")}";
        return outputString;
    }
}