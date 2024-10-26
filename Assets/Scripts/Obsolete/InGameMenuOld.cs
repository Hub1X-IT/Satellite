using UnityEngine;

public class InGameMenuOld : MonoBehaviour {
    

    [SerializeField] private Canvas playerPauseMenu;
    [SerializeField] private Canvas playerOptions;
    [SerializeField] private ObjectiveDisplayController objectiveDisplayController;


    private void Awake() {
        playerPauseMenu.gameObject.SetActive(false);
        playerOptions.gameObject.SetActive(false);
        objectiveDisplayController.enabled = true;
    }


    private void Start() {
        GameManager.Instance.OnGamePauseUnpause += PauseUnpause;
    }


    public void PauseUnpause(bool targetState) {        
        playerPauseMenu.gameObject.SetActive(targetState);
        playerOptions.gameObject.SetActive(false); // disable the options both when pausing and unpausing
        objectiveDisplayController.enabled = !targetState;
    }


    public void OpenOptions(bool targetState) {
        playerPauseMenu.gameObject.SetActive(!targetState);
        playerOptions.gameObject.SetActive(targetState);        
    }


    public void LoadMainMenu() {
        print("Loading Menu...");
    }
}
