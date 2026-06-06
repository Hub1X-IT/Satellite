using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

// Modified version of the default LinePresenter
public class OurDialogueLinePresenter : DialoguePresenterBase
{
    [SerializeField]
    private DialogueCharacterDatabase dialogueCharacterDatabase;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TMP_Text characterNameTextField;

    [SerializeField]
    private TMP_Text lineTextField;

    [SerializeField]
    private Image characterPortraitImage;

    [SerializeField]
    private bool useFadeEffect = true;

    [SerializeField]
    private float fadeUpDuration = 0.25f;

    [SerializeField]
    private float fadeDownDuration = 0.1f;

    [SerializeField]
    private ActionMarkupHandler advanceButton;

    public override YarnTask OnDialogueCompleteAsync()
    {
        canvasGroup.alpha = 0;
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        canvasGroup.alpha = 0;
        return YarnTask.CompletedTask;
    }

    public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        DialogueCharacterSO dialogueCharacterSO = dialogueCharacterDatabase.GetDialogueCharacterSO(line.CharacterName);

        if (dialogueCharacterSO != null)
        {
            characterNameTextField.text = dialogueCharacterSO.DisplayName;
            characterPortraitImage.sprite = dialogueCharacterSO.Portrait;
            characterPortraitImage.gameObject.SetActive(true);
        }
        else
        {
            characterNameTextField.text = line.CharacterName;
            characterPortraitImage.gameObject.SetActive(false);
            characterPortraitImage.sprite = null;
        }

        lineTextField.text = line.TextWithoutCharacterName.Text;

        // fade up the UI
        if (useFadeEffect)
        {
            await Effects.FadeAlphaAsync(canvasGroup, 0, 1, fadeUpDuration, token.HurryUpToken);
        }
        else
        {
            canvasGroup.alpha = 1;
        }

        await YarnTask.WaitUntilCanceled(token.NextContentToken).SuppressCancellationThrow();

        // fade down the UI
        if (useFadeEffect)
        {
            await Effects.FadeAlphaAsync(canvasGroup, 1, 0, fadeDownDuration, token.HurryUpToken).SuppressCancellationThrow();
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }
}
