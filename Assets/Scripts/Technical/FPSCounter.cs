using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int fps;

    [SerializeField]
    private TMP_Text fpsCounterText;

    private void Start()
    {
        InvokeRepeating(nameof(GetFPS), 1, 1);
    }

    public void SetFPSCounterActive(bool enabled)
    {
        fpsCounterText.gameObject.SetActive(enabled);
    }
    
    private void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsCounterText.text = "FPS: " + fps.ToString();
    }
}
