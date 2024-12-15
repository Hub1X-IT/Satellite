using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform detectionWarning;

    private void Awake()
    {
        DetectionManager.DetectionOccured += () => SetWarningUIEnabled(true);
        DetectionManager.DetectionRemoved += () => SetWarningUIEnabled(false);
    }

    private void SetWarningUIEnabled(bool enabled)
    {
        detectionWarning.sizeDelta = enabled ? new(280, 400) : new(-280, 400);
    }
}
