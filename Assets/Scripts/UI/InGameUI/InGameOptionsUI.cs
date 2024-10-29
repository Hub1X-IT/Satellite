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


    private void Awake()
    {
        inGameMenu = GetComponentInParent<InGameMenuUI>();

        inGameMenu.OptionsEnabled += (state) => gameObject.SetActive(state);

        backButton.onClick.AddListener(() => inGameMenu.SetOptionsEnabled(false));
    }
}