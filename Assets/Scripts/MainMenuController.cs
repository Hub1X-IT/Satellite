using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Button playButton;

    [SerializeField]
    Button quitButton;
    private void Awake()
    {
        playButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.PlayerHouse));
        quitButton.onClick.AddListener(() => Application.Quit());
    }
    void Update()
    {
        
    }
}
