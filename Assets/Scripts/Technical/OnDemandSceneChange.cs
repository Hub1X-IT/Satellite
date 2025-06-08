using UnityEngine;

public class OnDemandSceneChange : MonoBehaviour
{
    [SerializeField]
    private SceneLoader.Scene targetScene;

    public void OnDemandChangeScene()
    {
        SceneLoader.LoadScene(targetScene);
    }
}
