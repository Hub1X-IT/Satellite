using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField]
    private AudioSource deskSitAudioSource;

    // Needed for guidebook lookup from password cracking app
    [SerializeField]
    private Guidebook guidebook;

    public Guidebook Guidebook => guidebook;

    public void PlayDeskSitSound()
    {
        deskSitAudioSource.Play();
    }
}
