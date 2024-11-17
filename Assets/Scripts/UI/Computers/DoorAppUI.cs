using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorAppUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;

    string doorAppText = "Door Closed";
    bool isDoorOpen = false;

    [SerializeField]
    private Button doorButton;

    private void Awake()
    {
        doorButton.onClick.AddListener(Doorer);

        textField.text = doorAppText;
    }

    private void Doorer()
    {
        Debug.Log($"bazinga4");

        if (isDoorOpen == false)
        {
            isDoorOpen = true;
            doorAppText = "Door Open";
            textField.text = doorAppText;
        }
        else
        {
            isDoorOpen = false;
            doorAppText = "Door Closed";
            textField.text = doorAppText;
        }
    }
}
