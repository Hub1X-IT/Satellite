using System;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public event Action<bool> OnOptionsOpenClose;

    private void Awake()
    {
        GameManager.GamePausedUnpaused += (state) => gameObject.SetActive(state);
    }

    private void OnEnable()
    {
        // The options need to be closed when pausing.
        OpenCloseOptions(false);
    }

    public void OpenCloseOptions(bool state)
    {
        OnOptionsOpenClose?.Invoke(state);
    }
}