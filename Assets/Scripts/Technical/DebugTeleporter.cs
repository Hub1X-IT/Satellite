using System.Collections.Generic;
using UnityEngine;

public class DebugTeleporter : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField]
    private SerializableDictionary<KeyCode, Transform> serializableDictionary;

    private Dictionary<KeyCode, Transform> dictionary;

    private void Awake()
    {
        Debug.Log("Debug teleporter is active.");
        dictionary = serializableDictionary.Dictionary;
    }

    private void Update()
    {
        foreach (var key in dictionary.Keys)
        {
            if (Input.GetKeyDown(key) && GameManager.HiddenCursorLockMode == CursorLockMode.Locked)
            {
                playerMovementController.WarpPosition(dictionary[key].position);
                Debug.Log(playerMovementController.transform.position);
                break;
            }
        }
    }
}