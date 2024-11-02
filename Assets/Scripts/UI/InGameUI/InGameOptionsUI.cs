using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOptionsUI : MonoBehaviour
{
    private InGameMenuUI inGameMenu;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown graphicsDropdown;

    Resolution[] resolutions;


    private void Awake()
    {
        inGameMenu = GetComponentInParent<InGameMenuUI>();

        inGameMenu.OptionsEnabled += (state) => gameObject.SetActive(state);

        backButton.onClick.AddListener(() => inGameMenu.SetOptionsEnabled(false));

        graphicsDropdown.onValueChanged.AddListener(SetGraphics);
        //resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void Start()
    {
        SetGraphics(GameSettingsManager.GraphicsIndex);
        graphicsDropdown.RefreshShownValue();

        /*SetResolution(GameSettingsManager.ResolutionIndex);
        resolutionDropdown.RefreshShownValue();*/

        resolutions = UnityEngine.Device.Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i= 0; i < resolutions.Length; i++)
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

    private void SetGraphics(int index)
    {
        GameSettingsManager.SetGraphics(index);
    }
    
    /*private void SetResolution(int index)
    {
        GameSettingsManager.SetResolution(index);
    }*/
}