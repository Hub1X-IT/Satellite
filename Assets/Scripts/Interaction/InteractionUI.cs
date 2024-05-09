using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour  {


    [SerializeField] private TextMeshProUGUI interactionText;


    private string defaultText;
    

    // Hopefully a temporary solution
    [SerializeField] private string interactKeyString = "[F]";


    private void Awake() {
        defaultText = interactionText.text;
        DisableInteractionText();        
    }


    public void EnableInteractionText(string interactString) {
        interactionText.text = interactString + " " + interactKeyString;
        interactionText.gameObject.SetActive(true);
    }


    public void DisableInteractionText() {
        interactionText.text = defaultText;
        interactionText.gameObject.SetActive(false);
    }
}
