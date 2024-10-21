using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour  {


    [SerializeField] private TextMeshProUGUI interactionText;

    private string defaultText;    
        
    private const string INTERACT_KEY_STRING = "[E]";


    private void Awake() {
        defaultText = interactionText.text;
        DisableInteractionText();        
    }


    public void EnableInteractionText(string interactString) {
        interactionText.text = interactString + INTERACT_KEY_STRING;
        interactionText.gameObject.SetActive(true);
    }


    public void DisableInteractionText() {
        interactionText.text = defaultText;
        interactionText.gameObject.SetActive(false);
    }
}
