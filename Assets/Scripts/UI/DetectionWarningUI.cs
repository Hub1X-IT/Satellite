using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform detectionWarning;

    public void ShowHideWarning()
    {
        detectionWarning.sizeDelta = DetectionManager.WasDetected ? new Vector2(280, 400) : new Vector2(-280, 400);
    }
}
