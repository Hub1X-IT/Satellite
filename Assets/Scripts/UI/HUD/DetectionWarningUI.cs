using UnityEditor.Animations;
using UnityEditor.Rendering;
using UnityEngine;

public class DetectionWarningUI : MonoBehaviour
{
    [SerializeField]
    private Animator warningAnimator;

    private const string Show = "Show";
    private const string Hide = "Hide";


    private void Awake()
    {
        DetectionManager.DetectionOccured += () => SetWarningUIEnabled(true);
        DetectionManager.DetectionRemoved += () => SetWarningUIEnabled(false);
        //SetWarningUIEnabled(false);
    }

    private void SetWarningUIEnabled(bool enabled)
    {
        warningAnimator.SetTrigger(enabled ? Show : Hide);
        //gameObject.SetActive(enabled);
    }
}
