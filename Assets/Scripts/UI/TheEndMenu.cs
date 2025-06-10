using UnityEngine;
using UnityEngine.UI;

public class TheEndMenu : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MainMenu));
    }
}
