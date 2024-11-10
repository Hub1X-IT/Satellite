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
        quitButton.onClick.AddListener(() => Application.Quit());
    }
    void Update()
    {
        
    }
}
