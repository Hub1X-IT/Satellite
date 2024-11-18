using TMPro;
using UnityEngine;

public class MonitorUIFolder : MonoBehaviour
{
    [SerializeField]
    private TMP_Text folderNameTextField;

    public void SetFolderName(string folderName)
    {
        folderNameTextField.text = folderName;
    }
}