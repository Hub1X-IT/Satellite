using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private EnterableUIObject settingsMenu;

    [SerializeField]
    private EnterableUIObject creditsMenu;

    [SerializeField]
    private RectTransform loadingScreen;

    [SerializeField]
    private Slider loadingSlider;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { 
            DisableMainMenu();
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(SceneLoader.LoadSceneAsync(SceneLoader.Scene.PlayerHouse, loadingSlider)); 
        });

        playButton.onClick.AddListener(() => GameManager.PauseGameToMenu(false));

        settingsButton.onClick.AddListener(() =>
        {
            DisableMainMenu();
            settingsMenu.Enable(EnableMainMenu);
        });
        creditsButton.onClick.AddListener(() =>
        {
            DisableMainMenu();
            creditsMenu.Enable(EnableMainMenu);
        });
        quitButton.onClick.AddListener(Application.Quit);
    }

    private void EnableMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }

    private void DisableMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }
}
