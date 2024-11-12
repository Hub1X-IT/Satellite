using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform menuScreen;

    [SerializeField]
    private RectTransform settingsScreen;

    [SerializeField]
    private RectTransform creditsScreen;

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
            menuScreen.gameObject.SetActive(false);
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(SceneLoader.LoadSceneAsync(SceneLoader.Scene.PlayerHouse, loadingSlider)); 
        });
        settingsButton.onClick.AddListener(() =>
        {
            menuScreen.gameObject.SetActive(false);
            settingsScreen.gameObject.SetActive(true);
        });
        creditsButton.onClick.AddListener(() =>
        {
            menuScreen.gameObject.SetActive(false);
            creditsScreen.gameObject.SetActive(true);
        });
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
