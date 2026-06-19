using UnityEngine;

public class DetectionSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        DetectionManager.Instance.OnDetectionLevelIncreased += audioSource.Play;
    }
}
