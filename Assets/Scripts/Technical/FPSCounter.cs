using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;

    [SerializeField]
    private TMP_Text fpsCounterText;

    [SerializeField]
    private GraphicsSettingsUI graphicsSettingsUI;

    private void Awake()
    {
        graphicsSettingsUI.OnFPSDisplayToggled += (bool enabled) =>
        {
            SetFPSCounterActive(enabled);
        };
    }

    private void Start()
    {
        SetFPSCounterActive(GameSettingsManager.Instance.FPSDisplay);
        
        InvokeRepeating(nameof(GetFPS), 1, 1);
    }

    private void SetFPSCounterActive(bool enabled)
    {
        fpsCounterText.gameObject.SetActive(enabled);
    }
    
    private void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsCounterText.text = "FPS: " + fps.ToString();
    }
}
