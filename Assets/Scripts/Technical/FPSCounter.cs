using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;

    [SerializeField]
    private TMP_Text fpsCounterText;

    private void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }
    
    private void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsCounterText.text = "FPS: " + fps.ToString();
    }
}
