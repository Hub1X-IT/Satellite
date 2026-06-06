using UnityEngine;
using Yarn.Unity;

public class DialogueLineAdvancerHelper : MonoBehaviour
{
    [SerializeField]
    private LineAdvancer lineAdvancer;

    private void Start()
    {
        GameInput.Instance.OnDialogueSkipAction += () =>
        {
            // lineAdvancer.RequestLineHurryUp();
            lineAdvancer.RequestNextLine();
        };
    }
}
