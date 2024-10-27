using System;
using UnityEngine;

public class PlayerUIControllerOld : MonoBehaviour {

    
    public Action<bool> OnPauseUnpause;

    public bool CanShowPlayerHUD { get; private set; }

    private void Start() {
        GameManager.OnGamePauseUnpause += (bool targetState) => { OnPauseUnpause?.Invoke(targetState); };
    }

    public void SetCanShowPlayerHUD(bool targetState) {
        CanShowPlayerHUD = targetState;
    }
    
}