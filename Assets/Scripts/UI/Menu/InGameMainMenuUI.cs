using UnityEngine;
using UnityEngine.UI;

public class InGameMainMenuUI : MonoBehaviour
{
    [SerializeField]
    private EnterableUIObject inGameOptions;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button mainMenuButton;

    private void Start()
    {
        GameManager.Instance.GamePausedUnpaused += (paused) =>
        {
            SetEnabled(paused);
            inGameOptions.Disable();
        };

        resumeButton.onClick.AddListener(() => GameManager.Instance.PauseGameToMenu(false));

        optionsButton.onClick.AddListener(() =>
        {
            SetEnabled(false);
            inGameOptions.Enable(onCloseAction: () => SetEnabled(true));
        });

        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MainMenu));

        SetEnabled(GameManager.Instance.IsGamePaused);
        inGameOptions.Disable();
    }

    private void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}