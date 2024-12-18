using UnityEngine;
using UnityEngine.UI;

public class SingleEmailController : MonoBehaviour
{
    [SerializeField]
    private Button emailButton;

    [SerializeField]
    private RectTransform emailRectTransform;

    [SerializeField]
    private Sprite readEmailSprite;

    [SerializeField]
    private EnterableUIObject emailContent;

    [SerializeField]
    private GameEventSO objectiveEvent;

    private bool wasOpened = false;

    private void Awake()
    {
        emailButton.onClick.AddListener(() =>
        {
            if(objectiveEvent != null)
            {
                objectiveEvent.TryRaiseEvent();
                objectiveEvent = null;
            }
            DisableDay1EmailObject();
            if (wasOpened == false)
            {
                emailButton.image.sprite = readEmailSprite;
                wasOpened = true;
            }
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
