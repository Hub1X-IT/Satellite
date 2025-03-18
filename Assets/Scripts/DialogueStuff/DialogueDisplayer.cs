using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventReader : MonoBehaviour
{
    [Serializable]
    public class DialogueData
    {
        public GameEventSO[] GameEvents;
        public string Texticle;
    }

    [SerializeField]
    private TextMeshProUGUI DialogueDisplay;

    [SerializeField]
    private string DefaultText;

    [SerializeField]
    private SpriteRenderer DialogueBox;

    [SerializeField]
    private DialogueData[] DialoguesData;

    private void Awake()
    {
        //DialogueDisplay.enabled = false;
        //DialogueBox.enabled = false;
        DialogueShow(false);
        Debug.Log("bzinga");
    }

    public void DialogueShow(bool enabled)
    {
        DialogueDisplay.gameObject.SetActive(enabled);
        DialogueDisplay.text = DefaultText;
        DialogueBox.gameObject.SetActive(enabled);
    }
}
