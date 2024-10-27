using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader {

    public enum Scenes {
        MainMenu,
        PlayerHouse,
    }

    private static Scenes targetScene;

    public static void LoadScene(Scenes scene) {
        targetScene = scene;

        SceneManager.LoadScene(targetScene.ToString());
    }
}