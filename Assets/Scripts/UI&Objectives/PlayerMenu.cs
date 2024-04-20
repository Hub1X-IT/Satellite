using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
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
    public void Resume()
    {
        playerPauseMenu.SetActive(false);
        playerHud.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        playerPauseMenu.SetActive(true);
        playerOptions.SetActive(false);
        playerHud.enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Options()
    {
        playerOptions.SetActive(true);
        playerPauseMenu.SetActive(false);
    }
    public void Menu()
    {
        Debug.Log("Loading Menu...");
    }
}
