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

    private CinemachineBasicMultiChannelPerlinSettings currentSettings;

    private void Awake()
    {
        playerMovementController.StartedMoving += OnPlayerStartedMoving;

        cinemachineBasicMultiChannelPerlin = playerCinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        currentSettings = playerNotMovingSettings;
    }

    private void Update()
    {
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(cinemachineBasicMultiChannelPerlin.AmplitudeGain, currentSettings.AmplitudeGain, Time.deltaTime * lerpSpeed);
        cinemachineBasicMultiChannelPerlin.FrequencyGain = Mathf.Lerp(cinemachineBasicMultiChannelPerlin.FrequencyGain, currentSettings.FrequencyGain, Time.deltaTime * lerpSpeed);
    }

    private void OnDestroy()
    {
        playerMovementController.StartedMoving -= OnPlayerStartedMoving;
    }

    public void OnPlayerStartedMoving(bool isMoving)
    {
        currentSettings = isMoving ? playerMovingSettings : playerNotMovingSettings;
    }
}