using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private Slider loadingSlider;

    public void LoadLevelButton(string leverToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true); 

        //Run the A sync
        StartCoroutine(LoadLevelAsync(leverToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
