using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenu,
        PlayerHouse,
    }

    private static Scene targetScene;

    public static void LoadScene(Scene scene)
    {
        targetScene = scene;

        SceneManager.LoadScene(targetScene.ToString());
    }
}