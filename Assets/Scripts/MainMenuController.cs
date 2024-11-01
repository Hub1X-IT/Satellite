using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    Button playButton;
    private void Awake()
    {
        playButton = GetComponentInChildren<Button>();
        playButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.PlayerHouse));
    }
    void Update()
    {
        
    }
}
