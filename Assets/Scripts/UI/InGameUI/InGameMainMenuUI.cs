using UnityEngine;
using UnityEngine.UI;

public class InGameMainMenuUI : MonoBehaviour
{
    [SerializeField]
    private InGameOptionsUI inGameOptions;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button mainMenuButton;


    private void Awake()
    {
        GameManager.GamePausedUnpaused += (paused) =>
        {
            SetEnabled(paused);
            inGameOptions.Disable();
        };

        resumeButton.onClick.AddListener(() => GameManager.PauseGameToMenu(false));

        optionsButton.onClick.AddListener(() =>
        {
            SetEnabled(false);
            inGameOptions.Enable(onCloseAction: () => SetEnabled(true));
        });

        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MainMenu));
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}