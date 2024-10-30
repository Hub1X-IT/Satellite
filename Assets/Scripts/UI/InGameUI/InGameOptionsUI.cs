using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
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
    }

    private void Start()
    {
        SetGraphics(GameSettingsManager.GraphicsIndex);
        graphicsDropdown.RefreshShownValue();

        //resolutions = Screen.resolutions;
    }
    private void SetGraphics(int index)
    {
        GameSettingsManager.SetGraphics(index);
    }
}