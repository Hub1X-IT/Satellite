using System;
using UnityEngine;

public class InGameMenu : MonoBehaviour {

    private PlayerUIController playerUIController;

    public event Action<bool> OnOptionsOpenClose;

    private void Awake() {
        playerUIController = GetComponentInParent<PlayerUIController>();

        playerUIController.OnPauseUnpause += (bool targetState) => { gameObject.SetActive(targetState); };
    }

    private void OnEnable() { // the options need to be closed when pausing
        OpenOptions(false);
    }

    public void OpenOptions(bool targetState) {
        OnOptionsOpenClose(targetState);
    }
}