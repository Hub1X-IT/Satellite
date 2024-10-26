using UnityEngine;

public class PlayerMenuOld : MonoBehaviour
{
    /*
    public static bool GameIsPaused = false;
    public GameObject playerPauseMenu;
    public GameObject playerOptions;
    private MonoBehaviour playerHud;

    private void Awake()
    {
        playerHud = GetComponent<PlayerUI>();
    }
    private void Start()
    {
        playerPauseMenu.SetActive(false);
        playerOptions.SetActive(false);
        playerHud.enabled = true;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused && playerPauseMenu.activeSelf)
            {
                Resume();
            }
            else if((!GameIsPaused && !playerPauseMenu.activeSelf) || (GameIsPaused && playerOptions.activeSelf))
            {
                Pause();
            }
        }
    }
    private void Resume()
    {
        playerPauseMenu.SetActive(false);
        playerHud.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Pause()
    {
        playerPauseMenu.SetActive(true);
        playerOptions.SetActive(false);
        playerHud.enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Options()
    {
        playerOptions.SetActive(true);
        playerPauseMenu.SetActive(false);
    }
    private void Menu()
    {
        Debug.Log("Loading Menu...");
    }
    */
}
