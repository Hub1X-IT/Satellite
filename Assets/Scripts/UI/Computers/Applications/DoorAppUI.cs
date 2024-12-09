using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorAppUI : MonoBehaviour
{
    [SerializeField]
    private Button doorButton;

    [SerializeField]
    private TMP_Text doorAppTextField;

    // private string doorAppText;
    private bool isDoorOpen;

    private const string DoorClosedText = "Door Closed";
    private const string DoorOpenText = "Door Open";

    private void Awake()
    {
        doorButton.onClick.AddListener(() => SetDoorOpen(!isDoorOpen));

        SetDoorOpen(false);
    }

    private void SetDoorOpen(bool open)
    {
        isDoorOpen = open;
        // If bool (open) is true, assign the first value (DoorOpenText), else assign the second one (DoorClosedText)
        doorAppTextField.text = open ? DoorOpenText : DoorClosedText;
    }

    /*
    private void Doorer()
    {
        Debug.Log($"bazinga4");

        if (isDoorOpen == false)
        {
            isDoorOpen = true;
            doorAppText = "Door Open";
            doorAppTextField.text = doorAppText;
        }
        else
        {
            isDoorOpen = false;
            doorAppText = "Door Closed";
            doorAppTextField.text = doorAppText;
        }
    }
    */
}
