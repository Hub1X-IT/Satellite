using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    [SerializeField]
    private Animator warningAnimator;

    private const string Show = "Show";
    private const string Hide = "Hide";

    private bool shouldShowWarningUI;

    private void Start()
    {
        DetectionManager.Instance.OnDetectionOccured += () =>
        {
            shouldShowWarningUI = true;

            // must add auto hiding the warning UI after some time
        };
        //SetWarningUIEnabled(false);
    }

    private void LateUpdate()
    {
        if (shouldShowWarningUI)
        {
            SetWarningUIEnabled(true);
            shouldShowWarningUI = false;
        }
    }

    private void SetWarningUIEnabled(bool enabled)
    {
        warningAnimator.SetTrigger(enabled ? Show : Hide);
        //gameObject.SetActive(enabled);
    }
}
