using UnityEngine;

public class ScriptsInitializer : MonoBehaviour
{
    [SerializeField] private CameraController.InitializationData cameraControllerData;

    [SerializeField] private InteractionController.InitializationData interactionControllerData;

    [SerializeField] private VolumeController.InitializationData volumeControllerData;


    private void Awake()
    {
        GameInput.InitializeInput();

        CameraController.InitializeOnAwake(cameraControllerData);
        InteractionController.InitializeOnAwake(interactionControllerData);
        VolumeController.InitializeOnAwake(volumeControllerData);

        GameSettingsManager.LoadSettings();
    }

    private void Start()
    {
        CommandPromptManager.InitializeOnStart();
        GameManager.InitializeOnStart();
        InteractionController.InitializeOnStart();
        VolumeController.InitializeOnStart();
    }

    private void OnDestroy()
    {
        GameInput.RemoveInput();
    }
}
