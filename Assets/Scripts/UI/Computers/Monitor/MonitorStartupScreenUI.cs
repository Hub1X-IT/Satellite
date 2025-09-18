using System;
using UnityEngine;

public class MonitorStartupScreenUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Not connected to any server, no server used before")]
    private GameObject[] case1StartupScreenElements;

    [SerializeField]
    [Tooltip("Connection broken by detection or server disconnect")]
    private GameObject[] case2StartupScreenElements;

    [SerializeField]
    [Tooltip("Connected to server, but no command typed in (WIP)")]
    // ^ Command to be added
    private GameObject[] case3StartupScreenElements;

    [SerializeField]
    private float objectsActivationInterval;

    private float objectsActivationTimer;
    private bool shouldActivateObjects;
    private int currentObjectIndex;

    private GameObject[] currentSelectedStartupScreenElements;

    private Action startupScreenFinishedCallback;

    void Awake()
    {
        shouldActivateObjects = false;
        DisableAllObjects();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (shouldActivateObjects)
        {
            if (objectsActivationTimer <= 0)
            {
                objectsActivationTimer = objectsActivationInterval;
                if (currentObjectIndex >= currentSelectedStartupScreenElements.Length)
                {
                    shouldActivateObjects = false;
                    startupScreenFinishedCallback?.Invoke();
                    startupScreenFinishedCallback = null;
                    // Finished activating objects
                }
                else
                {
                    currentSelectedStartupScreenElements[currentObjectIndex].SetActive(true);
                    currentObjectIndex++;
                }
            }
            else
            {
                objectsActivationTimer -= Time.deltaTime;
            }
        }
    }

    public void StartStartupScreen(Action onFinishedStartupCallback)
    {
        gameObject.SetActive(true);
        startupScreenFinishedCallback = onFinishedStartupCallback;
        DisableAllObjects();

        if (!ServerConnectionManager.WasEverConnected && !ServerConnectionManager.IsConnectionActive)
        {
            currentSelectedStartupScreenElements = case1StartupScreenElements;
            Debug.Log("Selected case1");
        }
        else if (!ServerConnectionManager.IsConnectionActive || DetectionManager.WasDetected)
        // ^ Second condition may not be necessary
        {
            currentSelectedStartupScreenElements = case2StartupScreenElements;
            Debug.Log("Selected case2");
        }
        else if (ServerConnectionManager.IsConnectionActive)
        // ^ Needs adding more conditions based on typed commands
        {
            currentSelectedStartupScreenElements = case3StartupScreenElements;
            Debug.Log("Selected case3");
        }

        objectsActivationTimer = objectsActivationInterval;
        currentObjectIndex = 0;
        shouldActivateObjects = true;
    }

    private void DisableAllObjects()
    {
        foreach (var gameObject in case1StartupScreenElements)
        {
            gameObject.SetActive(false);
        }
        foreach (var gameObject in case2StartupScreenElements)
        {
            gameObject.SetActive(false);
        }
        foreach (var gameObject in case3StartupScreenElements)
        {
            gameObject.SetActive(false);
        }
    }
}