using DialogSystem.Runtime.Core;
using UnityEngine;

public class TempDialogSkipper : MonoBehaviour
{
    [SerializeField]
    private SmartphoneController smartphoneController;

    private void Start()
    {
        GameInput.Instance.OnDialogueSkipAction += () =>
        {
            if (!smartphoneController.IsSmartphoneEnabled)
            {
                DialogManager.Instance.SkipCurrentLine();
            }
        };
    }
}
