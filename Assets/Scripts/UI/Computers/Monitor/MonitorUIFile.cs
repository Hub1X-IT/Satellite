using TMPro;
using UnityEngine;

public class MonitorUIFile : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fileNameTextField;

    [SerializeField]
    private TMP_Text fileContentTextField;

    public void SetFileName(string fileName)
    {
        fileNameTextField.text = fileName;
    }

    public void SetFileContent(string fileContent)
    {
        fileContentTextField.text = fileContent;
    }
}