using UnityEngine;
using Yarn.Unity;

public class DialogueUIHelper : MonoBehaviour
{
    [SerializeField]
    private DialogueRunner dialogueRunner;

    [SerializeField]
    private CanvasGroup dialogueUICanvasGroup;

    private void Start()
    {
        dialogueRunner.onDialogueStart.AddListener(() => dialogueUICanvasGroup.blocksRaycasts = true);
        dialogueRunner.onDialogueComplete.AddListener(() => dialogueUICanvasGroup.blocksRaycasts = false);

        dialogueUICanvasGroup.blocksRaycasts = false;
    }
}
