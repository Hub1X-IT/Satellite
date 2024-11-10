using UnityEngine;

public class ScriptsInitializer : MonoBehaviour
{
    [SerializeField]
    private CameraController.InitializationData cameraControllerData;

    [SerializeField]
    private InteractionController.InitializationData interactionControllerData;

    [SerializeField]
    private VolumeController.InitializationData volumeControllerData;

    private void Awake()
    {
        GameInput.InitializeInput();

        CameraController.OnAwake(cameraControllerData);
        GameManager.OnAwake();
        GraphicsSettingsManager.OnAwake();
        InteractionController.OnAwake(interactionControllerData);
        VolumeController.OnAwake(volumeControllerData);

        GameSettingsManager.LoadSettings();
    }

    private void Start()
    {
        CommandPromptManager.OnStart();
        GameManager.OnStart();
        GraphicsSettingsManager.OnStart();
        VolumeController.OnStart();
    }

    private void OnDestroy()
    {
        CommandPromptManager.OnSceneExit();
        GameManager.OnSceneExit();

        GameInput.RemoveInput();

        GameSettingsManager.SaveSettings();
    }
}
