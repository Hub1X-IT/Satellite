using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour {


    private enum WhenToReset {
        Awake,
        Start,
        OnEnable,
        OnDisable,
    }

    [SerializeField] private WhenToReset whenToReset;

    [SerializeField] private GameObject gameObjectToReset;

    [SerializeField] private Vector3 targetRotation;


    private void Awake() {
        if (whenToReset == WhenToReset.Awake) {
            gameObjectToReset.transform.rotation = Quaternion.Euler(targetRotation);
        }
    }

    private void Start() {
        if (whenToReset == WhenToReset.Start) {
            gameObjectToReset.transform.rotation = Quaternion.Euler(targetRotation);
        }
    }

    private void OnEnable() {
        if (whenToReset == WhenToReset.OnEnable) {
            gameObjectToReset.transform.rotation = Quaternion.Euler(targetRotation);
        }
    }

    private void OnDisable() {
        if (whenToReset == WhenToReset.OnDisable) {
            gameObjectToReset.transform.rotation = Quaternion.Euler(targetRotation);
        }
    }
}