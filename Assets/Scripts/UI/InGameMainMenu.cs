using UnityEngine;
using UnityEngine.UI;

public class InGameMainMenu : MonoBehaviour {

    private InGameMenu inGameMenu;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake() {
        inGameMenu = GetComponentInParent<InGameMenu>();

        inGameMenu.OnOptionsOpenClose += (bool targetState) => { gameObject.SetActive(!targetState); };

        resumeButton.onClick.AddListener(() => { GameManager.PauseGameToMenu(false); Debug.Log("PauseGame"); });
        optionsButton.onClick.AddListener(() => { inGameMenu.OpenOptions(true); });
        // mainMenuButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scenes.MainMenu); });
    }
}