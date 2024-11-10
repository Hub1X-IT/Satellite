using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform menuScreen;

    [SerializeField]
    private RectTransform loadingScreen;

    [SerializeField]
    private Slider loadingSlider;

    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { 
            menuScreen.gameObject.SetActive(false);
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(SceneLoader.LoadSceneAsync(SceneLoader.Scene.PlayerHouse, loadingSlider)); 
        });
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
