using System;
using UnityEngine;

public class InGameMenu : MonoBehaviour {

    public event Action<bool> OnOptionsOpenClose;

    private void Awake() {
        GameManager.OnGamePauseUnpause += (bool targetState) => { gameObject.SetActive(targetState); };
    }

    private void OnEnable() { // the options need to be closed when pausing
        OpenOptions(false);
    }

    public void OpenOptions(bool targetState) {
        OnOptionsOpenClose?.Invoke(targetState);
    }
}