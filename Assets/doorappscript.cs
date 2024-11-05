using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class doorappscript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textbox;

    string DoorAppText = "Door Closed";
    bool DoorOpen = false;

    [SerializeField]
    private Button doorbutton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        doorbutton.onClick.AddListener(() => Debug.Log($"bazinga4"));
        doorbutton.onClick.AddListener(() => Doorer());

        textbox.text = DoorAppText;
    }

    private void Doorer()
    {
        if (DoorOpen == false)
        {
            DoorOpen = true;
            DoorAppText = "Door Open";
            textbox.text = DoorAppText;
        }
        else
        {
            DoorOpen = false;
            DoorAppText = "Door Closed";
            textbox.text = DoorAppText;
        }
    }
}
