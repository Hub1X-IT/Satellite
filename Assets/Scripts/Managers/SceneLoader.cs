using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenu,
        PlayerHouse,
        TheEnd,
    }

    private static Scene targetScene;

    public static void LoadScene(Scene scene)
    {
        targetScene = scene;

        SceneManager.LoadScene(targetScene.ToString());
    }

    public static IEnumerator LoadSceneAsync(Scene scene, Slider loadingSlider)
    {
        targetScene = scene;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetScene.ToString());

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}