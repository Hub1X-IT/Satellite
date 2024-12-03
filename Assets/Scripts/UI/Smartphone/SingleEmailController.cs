using UnityEngine;
using UnityEngine.UI;

public class SingleEmailController : MonoBehaviour
{
    [SerializeField]
    private Button emailButton;

    [SerializeField]
    private RectTransform emailRectTransform;

    [SerializeField]
    private EnterableUIObject emailContent;

    [SerializeField]
    private GameEventSO objectiveEvent;

    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            if(objectiveEvent != null)
            {
                objectiveEvent.RaiseEvent();
                objectiveEvent = null;
            }
            DisableDay1EmailObject();
            emailContent.Enable(EnableDay1EmailObject);
        });
    }

    private void EnableDay1EmailObject()
    {
        emailRectTransform.gameObject.SetActive(true);
    }
    private void DisableDay1EmailObject()
    {
        emailRectTransform.gameObject.SetActive(false);
    }
}
