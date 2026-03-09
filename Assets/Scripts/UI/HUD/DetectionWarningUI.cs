using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    [SerializeField]
    private Animator warningAnimator;

    private const string ShouldShowParameter = "ShouldShow";

    private void Start()
    {
        DetectionManager.Instance.OnDetectionWarningStateChanged += SetWarningUIEnabled;

        SetWarningUIEnabled(false);
    }

    private void SetWarningUIEnabled(bool enabled)
    {
        warningAnimator.SetBool(ShouldShowParameter, enabled);
    }
}
