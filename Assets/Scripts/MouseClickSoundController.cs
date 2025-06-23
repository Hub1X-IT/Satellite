using UnityEngine;

public class MouseClickSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource mouseClickSoundAudioSource;

    private void Start()
    {
        GameInput.OnLeftClickPerformedAction += () =>
        {
            mouseClickSoundAudioSource.Play();
        };
    }
}
