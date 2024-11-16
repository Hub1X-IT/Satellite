using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    private ComputerUI computerUI;

    [SerializeField]
    private ComputerUICursorController monitorCursor;

    
    [SerializeField]
    private Button appsButton;

    [SerializeField]
    private Button filesButton;


    [SerializeField]
    private Canvas appsCanvas;

    [SerializeField]
    private Canvas filesCanvas;


    private bool appsEnabled;

    private bool filesEnabled;



    private void Awake()
    {
        computerUI = GetComponent<ComputerUI>();

        computerUI.ComputerViewEnabled += (enabled) =>
        {
            monitorCursor.SetEnabled(enabled);
        };

        
        
        appsButton.onClick.AddListener(AppsEnable);
        filesButton.onClick.AddListener(FilesEnable);



        //appsButton.onClick.AddListener(() => SetAppsEnabled(!appsEnabled));
        //filesButton.onClick.AddListener(() => SetFilesEnabled(!filesEnabled));


        
        //SetAppsEnabled(false);
        //SetFilesEnabled(false);




        appsCanvas.enabled = false;
        filesCanvas.enabled = false;


        monitorCursor.SetEnabled(false);
    }


    private void CloseAll()
    {
        appsCanvas.enabled = false;
        filesCanvas.enabled = false;
    }


    private void AppsEnable()
    {
        Debug.Log("bazinga2");

        if (appsCanvas.enabled == false)
        {
            CloseAll();
            appsCanvas.enabled = true;
        }
        else
        {
            appsCanvas.enabled = false;
        }
    }

    private void FilesEnable()
    {
        Debug.Log("bazinga3");

        if (filesCanvas.enabled == false)
        {
            CloseAll();
            filesCanvas.enabled = true;
        }
        else
        {
            filesCanvas.enabled = false;
        }
    }



    /*
    private void SetAppsEnabled(bool enabled)
    {
        CloseAll();
        appsCanvas.enabled = enabled;
        appsEnabled = enabled;
    }

    private void SetFilesEnabled(bool enabled)
    {
        CloseAll();
        filesCanvas.enabled = enabled;
        filesEnabled = enabled;
    }
    */

}