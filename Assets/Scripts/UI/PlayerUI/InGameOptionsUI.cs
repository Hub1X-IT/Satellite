using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOptionsUI : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown graphicsDropdown;

    private Resolution[] resolutions;

    private Action inGameOptionsClosed;

    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            Disable();
            inGameOptionsClosed();
        });

        graphicsDropdown.onValueChanged.AddListener(SetGraphics);

        // resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void Start()
    {
        SetGraphics(GameSettingsManager.GraphicsIndex);
        graphicsDropdown.RefreshShownValue();

        /*
        SetResolution(GameSettingsManager.ResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        */

        resolutions = UnityEngine.Device.Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == UnityEngine.Device.Screen.currentResolution.width
                && resolutions[i].height == UnityEngine.Device.Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Enable(Action onCloseAction)
    {
        inGameOptionsClosed = onCloseAction;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void SetGraphics(int index)
    {
        GameSettingsManager.SetGraphics(index);
    }

    /*
    private void SetResolution(int index)
    {
        GameSettingsManager.SetResolution(index);
    }
    */
}