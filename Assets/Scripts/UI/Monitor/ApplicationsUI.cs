using UnityEngine;
using UnityEngine.UI;
public class ApplicationsUI : MonoBehaviour
{
    [SerializeField]
    private Button doorAppButton;

    [SerializeField]
    private Canvas doorAppCanvas;

    private bool doorAppEnabled;

    private void Awake()
    {
        doorAppCanvas.enabled = false;

        doorAppButton.onClick.AddListener(DoorAppEnable);

        //doorAppButton.onClick.AddListener(() => SetDoorAppEnabled(!doorAppEnabled));

        //SetDoorAppEnabled(false);
    }

    private void CloseAll()
    {
        doorAppCanvas.enabled = false;
    }

    private void DoorAppEnable()
    {
        Debug.Log("bazinga4");

        if (doorAppCanvas.enabled == false)
        {
            CloseAll();
            doorAppCanvas.enabled = true;
        }
        else
        {
            doorAppCanvas.enabled = false;
        }
    }
    /*
    private void SetDoorAppEnabled(bool enabled)
    {
        CloseAll();
        doorAppCanvas.enabled = enabled;
        doorAppEnabled = enabled;
    }*/
}
