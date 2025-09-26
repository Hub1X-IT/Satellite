using System.Collections;
using UnityEngine;

public class TempGameEnd : MonoBehaviour
{
    [SerializeField]
    private GameEventSO gameEndTrigger;

    [SerializeField]
    private OnDemandSceneChange onDemandSceneChange;

    private void Awake()
    {
        gameEndTrigger.EventRaised += () =>
        {
            StartCoroutine(ChangeSceneCoroutine());
        };
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(1.4f);
        onDemandSceneChange.OnDemandChangeScene();
    }
}