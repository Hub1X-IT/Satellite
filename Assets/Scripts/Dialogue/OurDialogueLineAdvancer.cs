using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class OurDialogueLineAdvancer : MonoBehaviour
{
    [SerializeField]
    private DialogueRunner dialogueRunner;

    [SerializeField]
    private Button continueButton;

    private void Start()
    {
        GameInput.Instance.OnDialogueSkipAction += RequestNextLine;
        continueButton.onClick.AddListener(RequestNextLine);

        dialogueRunner.onDialogueStart.AddListener(() => SetContinueButtonEnabled(true));
        dialogueRunner.onDialogueComplete.AddListener(() => SetContinueButtonEnabled(false));

        SetContinueButtonEnabled(false);
    }

    private void RequestNextLine()
    {
        dialogueRunner.RequestNextLine();
    }

    private void SetContinueButtonEnabled(bool enabled)
    {
        continueButton.interactable = enabled;
        continueButton.enabled = enabled;
    }
}
