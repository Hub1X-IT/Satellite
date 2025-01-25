using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    private void Awake()
    {
        DetectionManager.DetectionOccured += () => SetWarningUIEnabled(true);
        DetectionManager.DetectionRemoved += () => SetWarningUIEnabled(false);
        SetWarningUIEnabled(false);
    }

    private void SetWarningUIEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
