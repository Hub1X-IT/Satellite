using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class notepadscript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textbox;

    string NotepadText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        NotepadText = "Bazoopa";
        textbox.text = NotepadText;
    }

  
}
