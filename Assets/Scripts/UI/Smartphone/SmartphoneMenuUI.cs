using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneMenuUI : MonoBehaviour
{
    [Serializable]
    private class AppEntry
    {
        public EnterableUIObject ui;
        public Button button;
        public GameObject notificationCircle;
        public TMP_Text notificationCount;
    }

    [SerializeField]
    private SerializableDictionary<string, AppEntry> apps;

    private void Awake()
    {
        foreach (var item in apps.Dictionary)
        {
            item.Value.button.onClick.AddListener(() =>
            {
                //SetEnabled(false);
                item.Value.ui.Enable(() => SetEnabled(true));
            });
        }
    }

    public void GoToMainMenu()
    {
        // Disable all objects except main menu
        foreach (var item in apps.Dictionary)
        {
            item.Value.ui.Disable();
        }

        SetEnabled(true);
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    public void SetNotificationCount(string app, int count)
    {
        AppEntry appObj = apps.Dictionary.GetValueOrDefault(app);

        if (appObj == null)
        {
            Debug.LogWarning($"Non-existent app called '{app}'");
            return;
        }

        appObj.notificationCircle?.SetActive(count != 0);
        if (appObj.notificationCount != null) appObj.notificationCount.text = count.ToString();
    }
}