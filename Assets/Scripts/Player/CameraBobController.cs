using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraBobController : MonoBehaviour
{
    [Serializable]
    private class CinemachineBasicMultiChannelPerlinSettings
    {
        public float AmplitudeGain;
        public float FrequencyGain;
    }

    [SerializeField]
    private CinemachineCamera playerCinemachineCamera;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlinSettings playerNotMovingSettings;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlinSettings playerMovingSettings;

    [SerializeField]
    private CinemachineBasicMultiChannelPerlinSettings headBobDisabledSettings;

    private CinemachineBasicMultiChannelPerlinSettings currentSettings;

    private bool isHeadBobEnabled;

    private void Awake()
    {
        playerMovementController.StartedMoving += OnPlayerStartedMoving;

        cinemachineBasicMultiChannelPerlin = playerCinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        currentSettings = playerNotMovingSettings;

        // Should be set by GraphicsSettingManager on Start anyway
        isHeadBobEnabled = false;
    }

    private void Update()
    {
        if (isHeadBobEnabled)
        {
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(cinemachineBasicMultiChannelPerlin.AmplitudeGain, currentSettings.AmplitudeGain, Time.deltaTime * lerpSpeed);
            cinemachineBasicMultiChannelPerlin.FrequencyGain = Mathf.Lerp(cinemachineBasicMultiChannelPerlin.FrequencyGain, currentSettings.FrequencyGain, Time.deltaTime * lerpSpeed);
        }
    }

    private void OnDestroy()
    {
        playerMovementController.StartedMoving -= OnPlayerStartedMoving;
    }

    public void OnPlayerStartedMoving(bool isMoving)
    {
        // currentSettings = isMoving ? playerMovingSettings : playerNotMovingSettings;
    }

    public void SetHeadBobEnabled(bool enabled)
    {
        isHeadBobEnabled = enabled;
        if (!enabled)
        {
            cinemachineBasicMultiChannelPerlin.AmplitudeGain = headBobDisabledSettings.AmplitudeGain;
            cinemachineBasicMultiChannelPerlin.FrequencyGain = headBobDisabledSettings.FrequencyGain;
        }
    }
}