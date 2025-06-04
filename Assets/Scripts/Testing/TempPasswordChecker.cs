using System;
using UnityEngine;

public class TempPasswordChecker : MonoBehaviour
{
    public static string CorrectPassword { get; set; }
    [SerializeField]
    private GameEventSO correctPasswordGuessedGameEvent;

    private static PasswordCrackingAppUI passwordCrackingApp;

    private static Action makeEventReference;

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
            correctPasswordGuessedGameEvent.TryRaiseEvent();
        }

    }
}