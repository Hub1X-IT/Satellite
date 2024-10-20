using UnityEngine;

public class InGameMenu : MonoBehaviour {
    

    [SerializeField] private Canvas playerPauseMenu;
    [SerializeField] private Canvas playerOptions;
    [SerializeField] private ObjectiveDisplayController objectiveDisplayController;


    private void Awake() {
        playerPauseMenu.gameObject.SetActive(false);
        playerOptions.gameObject.SetActive(false);
        objectiveDisplayController.enabled = true;
    }


    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction; // may be a temporary solution - moving to GameManager should be considered
    }


    private void GameInput_OnPauseAction(object sender, System.EventArgs e) {
        Pause(!GameManager.Instance.GamePaused);
    }


    public void Pause(bool targetState) {        
        playerPauseMenu.gameObject.SetActive(targetState);
        playerOptions.gameObject.SetActive(false); // disable the options both when pausing and unpausing
        objectiveDisplayController.enabled = !targetState;
        GameManager.Instance.PauseGame(targetState);
    }


    public void OpenOptions(bool targetState) {
        playerPauseMenu.gameObject.SetActive(!targetState);
        playerOptions.gameObject.SetActive(targetState);        
    }


    public void LoadMainMenu() {
        print("Loading Menu...");
    }
}
