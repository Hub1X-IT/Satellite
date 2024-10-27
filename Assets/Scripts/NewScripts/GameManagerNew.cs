using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

public class GameManagerNew : MonoBehaviour {
    // Game controller/initializer ?

    [SerializeField] private Camera mainCamera;

    [SerializeField] private CinemachineCamera cinemachineMainCamera;

    [SerializeField] private float interactRange;

    [SerializeField][Tooltip("Only one should be selected!")] private LayerMask defaultInteractableLayerMask;
    [SerializeField][Tooltip("Select also the layers that interaction should not pass through")] private LayerMask interactableLayerMasks;

    [SerializeField] private AudioMixer mainVolumeAudioMixer;
    [SerializeField] private AudioMixer musicVolumeAudioMixer;
    [SerializeField] private AudioMixer soundVolumeAudioMixer;

    private void Awake() {
        GameInput.InitializeInput();

        GameController.InitializeOnAwake();
        CameraControllerNew.InitializeOnAwake(mainCamera, cinemachineMainCamera);
        InteractionControllerNew.InitializeOnAwake(interactRange, defaultInteractableLayerMask, interactableLayerMasks);
        VolumeControllerNew.InitializeOnAwake(mainVolumeAudioMixer, musicVolumeAudioMixer, soundVolumeAudioMixer);

        GameSettingsManager.LoadSettings();
    }

    private void Start() {
        GameController.InitializeOnStart();
        CameraControllerNew.InitializeOnStart();
        InteractionControllerNew.InitializeOnStart();
    }

    private void OnDestroy() {
        GameInput.RemoveInput();
    }
}
