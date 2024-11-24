using UnityEngine;
using UnityEngine.UI;

public class OpenEmailContent : MonoBehaviour
{
    [SerializeField]
    private Button emailButton;

    [SerializeField]
    private GameObject emailButtonObject;

    [SerializeField]
    private GameObject emailContent;

    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            emailContent.SetActive(true);
            emailButtonObject.SetActive(false);
        });
    }
}
