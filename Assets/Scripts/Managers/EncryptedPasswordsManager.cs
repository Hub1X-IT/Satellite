using System.Collections.Generic;
using UnityEngine;

public class EncryptedPasswordsManager : MonoBehaviour
{
    private static List<EncryptedPassword> encryptedPasswords;

    public static List<EncryptedPassword> EncryptedPasswords => encryptedPasswords;

    private void Awake()
    {
        ResetPasswordsList();
    }

    public static void AddEncryptedPassword(EncryptedPassword encryptedPassword)
    {
        encryptedPasswords.Add(encryptedPassword);
    }

    public static void ResetPasswordsList()
    {
        encryptedPasswords = new();
    }
}
