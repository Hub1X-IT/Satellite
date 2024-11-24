using UnityEngine;
using UnityEngine.UI;

public class D1EmailEventTrigger : MonoBehaviour
{
    [SerializeField]
    private Button day1EmailButton;

    [SerializeField]
    private Component eventTrigger;

    [SerializeField]
    private GameEventSO openComputer;

    private void Awake()
    {
        day1EmailButton.onClick.AddListener(() =>
        {
            openComputer.RaiseEvent();
            Destroy(eventTrigger);
        });
    }
}
