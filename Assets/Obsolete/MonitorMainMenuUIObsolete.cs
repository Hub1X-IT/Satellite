using UnityEngine;
using UnityEngine.UI;

public class MonitorMainMenuUIObsolete : MonoBehaviour
{
    [SerializeField]
    private Button applicationsButton;
    [SerializeField]
    private Button filesButton;
    [SerializeField]
    private ApplicationsMenuUI applicationsMenu;
    [SerializeField]
    private FilesMenuUI filesMenu;

    private bool isApplicationsMenuEnabled;
    private bool isFilesMenuEnabled;

    private void Awake()
    {
        applicationsButton.onClick.AddListener(() => SetApplicationsMenuEnabled(!isApplicationsMenuEnabled));
        filesButton.onClick.AddListener(() => SetFilesMenuEnabled(!isFilesMenuEnabled));

        CloseAll();
    }

    private void CloseAll()
    {
        applicationsMenu.gameObject.SetActive(false);
        filesMenu.gameObject.SetActive(false);
    }

    private void SetApplicationsMenuEnabled(bool enabled)
    {
        CloseAll();
        applicationsMenu.gameObject.SetActive(enabled);
        isApplicationsMenuEnabled = enabled;
    }

    private void SetFilesMenuEnabled(bool enabled)
    {
        CloseAll();
        filesMenu.gameObject.SetActive(enabled);
        isFilesMenuEnabled = enabled;
    }

    /*
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
    */
}
