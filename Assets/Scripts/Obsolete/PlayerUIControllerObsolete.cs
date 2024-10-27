using System;
using UnityEngine;

public class PlayerUIControllerObsolete : MonoBehaviour
{
    public Action<bool> OnPauseUnpause;

    public bool CanShowPlayerHUD { get; private set; }

    private void Start()
    {
        GameManager.GamePausedUnpaused += (bool targetState) => { OnPauseUnpause?.Invoke(targetState); };
    }

    public void SetCanShowPlayerHUD(bool targetState)
    {
        CanShowPlayerHUD = targetState;
    }

}