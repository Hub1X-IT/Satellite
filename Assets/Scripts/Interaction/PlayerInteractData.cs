using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractData : MonoBehaviour {


    public static PlayerInteractData Instance { get; private set; }


    public Transform cameraFollowObject;
    public LayerMask interactableLayerMasks;
    public float interactRange;


    private void Awake() {
        Instance = this;
    }
}