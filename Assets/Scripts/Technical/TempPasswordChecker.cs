using System;
using System.Collections;
using UnityEngine;

public class TempPasswordChecker : MonoBehaviour
{
    public static string CorrectPassword { get; set; }
    [SerializeField]
    private GameEventSO correctPasswordGuessedGameEvent;

    private static PasswordCrackingAppUI passwordCrackingApp;

    private static Action makeEventReference;

    // very temporary
    private const float DecodingMessageShowTime = 1f;

    private void Awake()
    {
        makeEventReference = () =>
        {
            passwordCrackingApp.NewPasswordConverted += CheckPassword;
        };
    }

    private void OnDestroy()
    {
        makeEventReference = null;
    }

    public static void SetPasswordCrackingAppReference(PasswordCrackingAppUI app)
    {
        passwordCrackingApp = app;
        makeEventReference();
    }

    private void CheckPassword(string password)
    {
        if (password == CorrectPassword && correctPasswordGuessedGameEvent != null)
        {
            Debug.Log("correct password!");
            StartCoroutine(RaiseCorrectPasswordEvent());
        }

    }

    private IEnumerator RaiseCorrectPasswordEvent()
    {
        yield return new WaitForSeconds(DecodingMessageShowTime);
        correctPasswordGuessedGameEvent.TryRaiseEvent();
    }
}