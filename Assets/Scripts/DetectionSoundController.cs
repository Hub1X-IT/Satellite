using UnityEngine;

public class DetectionSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        DetectionManager.Instance.OnDetectionChanceChanged += (chance) =>
        {
            audioSource.Play();
        };
    }
}
