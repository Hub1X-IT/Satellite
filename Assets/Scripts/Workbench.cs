using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable {


    public void Interact() {
        Debug.Log("Workbench: Interact()");
    }


    public Transform GetTransform() {
        return transform;
    }
}
