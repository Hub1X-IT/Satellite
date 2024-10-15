using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationReset : MonoBehaviour {

    private enum WhenToReset {
        Awake,
        Start,
        OnEnable,
        OnDisable,
    }

    private enum RotationType {
        GlobalRotation,
        LocalRotation,        
    }

    [SerializeField] private WhenToReset whenToReset;
    [SerializeField] private RotationType rotationTypeToReset;
    [SerializeField] private GameObject gameObjectToReset;
    [SerializeField] private Vector3 targetRotation;


    private void Awake() { if (whenToReset == WhenToReset.Awake) ResetRotation(); }

    private void Start() { if (whenToReset == WhenToReset.Start) ResetRotation(); }

    private void OnEnable() { if (whenToReset == WhenToReset.OnEnable) ResetRotation(); }

    private void OnDisable() { if (whenToReset == WhenToReset.OnDisable) ResetRotation(); }

    private void ResetRotation() {
        if (rotationTypeToReset == RotationType.GlobalRotation) gameObjectToReset.transform.rotation = Quaternion.Euler(targetRotation);
        else if (rotationTypeToReset == RotationType.LocalRotation) gameObjectToReset.transform.localRotation = Quaternion.Euler(targetRotation);
    }
}