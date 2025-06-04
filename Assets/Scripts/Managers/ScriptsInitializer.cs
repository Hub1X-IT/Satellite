using UnityEngine;

public class ScriptsInitializer : MonoBehaviour
{
    [SerializeField]
    private CameraController.InitializationData cameraControllerData;

    [SerializeField]
    private CommandPromptManager.InitializationData commandPromptManagerData;

    [SerializeField]
    private DetectionManager.InitializationData detectionManagerData;

    [SerializeField]
    private GameEventOrderManager.InitializationData gameEventOrderManagerData;

    [SerializeField]
    private InteractionController.InitializationData interactionControllerData;

    [SerializeField]
    private VolumeController.InitializationData volumeControllerData;

    private void Awake()
    {
        GameInput.InitializeInput();

        CameraController.OnAwake(cameraControllerData);
        CommandPromptManager.OnAwake(commandPromptManagerData);
        DetectionManager.InitializeDetectionManager(detectionManagerData);
        GameEventOrderManager.OnAwake(gameEventOrderManagerData);
        GameManager.OnAwake();
        GraphicsSettingsManager.OnAwake();
        InteractionController.OnAwake(interactionControllerData);
        VirtualClipboard.InitializeVirtualClipboard();
        VolumeController.OnAwake(volumeControllerData);

        GameSettingsManager.LoadSettings();
    }

    private void Start()
    {
        GameManager.OnStart();
        GraphicsSettingsManager.OnStart();
        VolumeController.OnStart();
    }

    private void OnDestroy()
    {
        GameManager.OnSceneExit();
        DetectionManager.OnSceneExit();

        GameInput.RemoveInput();

        // GameSettingsManager.SaveSettings();
    }
}
