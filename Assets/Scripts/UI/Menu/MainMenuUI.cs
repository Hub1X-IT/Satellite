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

    [Header("Buttons")]
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitButton;

    [Header("Media Buttons")]
    [SerializeField]
    private Button discordButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            DisableMainMenu();
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(SceneLoader.LoadSceneAsync(SceneLoader.Scene.IntroLevel, loadingSlider));
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

        discordButton.onClick.AddListener(() =>
        {
            Application.OpenURL("https://discord.gg/BVk96xqx6m");
        });
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
