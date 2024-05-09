using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Smartphone : MonoBehaviour, IInteractable {


    public Transform GetTransform() {
        return transform;
    }


    public void Interact() {
        gameObject.SetActive(false);
    }
}
