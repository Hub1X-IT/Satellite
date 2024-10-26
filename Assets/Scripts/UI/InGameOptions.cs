using UnityEngine;
using UnityEngine.UI;

public class InGameOptions : MonoBehaviour {

    private InGameMenu inGameMenu;

    [SerializeField] private Button backButton;

    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Dropdown graphicsDropdown;

    private void Awake() {
        inGameMenu = GetComponentInParent<InGameMenu>();

        inGameMenu.OnOptionsOpenClose += (bool targetState) => { gameObject.SetActive(targetState); };

        backButton.onClick.AddListener(() => inGameMenu.OpenOptions(false));
    }
}