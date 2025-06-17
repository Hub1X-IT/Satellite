using System;
using UnityEngine;
using UnityEngine.UI;

public class SMSUI : MonoBehaviour
{
    /*
    [SerializeField]
    private Button smsExitButton;
    */

    private Action smsOverviewClosed;


    private void Awake()
    {
        /*
        smsExitButton.onClick.AddListener(() =>
        {
            smsOverviewClosed();
            Disable();
        });
        */
    }

    public void Enable(Action onCloseAction)
    {
        smsOverviewClosed = onCloseAction;

        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}