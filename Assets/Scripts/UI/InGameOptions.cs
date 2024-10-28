using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameOptions : MonoBehaviour
{
    private InGameMenu inGameMenu;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown graphicsDropdown;


    private void Awake()
    {
        inGameMenu = GetComponentInParent<InGameMenu>();

        inGameMenu.OptionsOpenedClosed += (state) => gameObject.SetActive(state);

        backButton.onClick.AddListener(() => inGameMenu.AreOptionsOpen = false);
    }
}