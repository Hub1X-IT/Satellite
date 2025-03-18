using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField]
    private AudioSource deskSitAudioSource;

    public void PlayDeskSitSound()
    {
        deskSitAudioSource.Play();
    }
}
