using System;
using UnityEngine;

public class PlayerUIController : MonoBehaviour {

    public Action<bool> OnPauseUnpause;

    private void Start() {
        GameManager.Instance.OnGamePauseUnpause += (bool targetState) => { OnPauseUnpause?.Invoke(targetState); };
    }
}