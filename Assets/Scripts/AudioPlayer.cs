using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource.Play();
    }
}
