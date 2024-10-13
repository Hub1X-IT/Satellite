using System;
using System.Collections;
using UnityEngine;

public class Smartphone : MonoBehaviour, IInteractable {


    public Transform GetTransform() {
        return transform;
    }


    public void Interact() {
        gameObject.SetActive(false);
    }
}
