using UnityEngine;

public class PlayerHUDController : MonoBehaviour {

    private PlayerUIController playerUIController;

    private void Awake() {
        playerUIController = GetComponentInParent<PlayerUIController>();

        playerUIController.OnPauseUnpause += (bool targetState) => { gameObject.SetActive(!targetState); };
    }
}
