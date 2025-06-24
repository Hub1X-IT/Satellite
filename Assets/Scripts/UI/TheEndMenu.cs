using UnityEngine;
using UnityEngine.UI;

public class TheEndMenu : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuButton;

    void Start()
    {
        GameManager.SetCursorShown(true);

        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene(SceneLoader.Scene.MainMenu));
    }
}
