using UnityEngine;
using UnityEngine.UI;

public class InGameMainMenuUI : MonoBehaviour
{
    private InGameMenuUI inGameMenu;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button mainMenuButton;


    private void Awake()
    {
        inGameMenu = GetComponentInParent<InGameMenuUI>();

        inGameMenu.OptionsEnabled += (areOptionsEnabled) => gameObject.SetActive(!areOptionsEnabled);

        resumeButton.onClick.AddListener(() => GameManager.PauseGameToMenu(false));
        optionsButton.onClick.AddListener(() => inGameMenu.SetOptionsEnabled(true));
        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MainMenu));
    }
}