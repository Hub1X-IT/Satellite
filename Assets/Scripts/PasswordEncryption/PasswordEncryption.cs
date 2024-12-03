using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PasswordEncryption
{
    public enum Cipher
    {
        None,
        ASCII_Base2,
        ASCII_Base8,
        ASCII_Base10,
        ASCII_Base16,
        AtbashCipher,
        CaesarCipher,
    }

    /*
        Case:
        0: No ciphers used, every cipher allowed.
        1: An ASCII cipher was used: the next cipher has to be one of the ASCII ciphers.
        2: Atbash cipher was used, the next one can't be Caesar.
        3: Caesar cipher was used, the next one can't be Atbash.
    */

    // private static readonly Cipher[] case0OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.CaesarCipher, Cipher.AtbashCipher };
    private static readonly Cipher[] case0OutputCiphers = { Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.CaesarCipher, Cipher.AtbashCipher };
    // private static readonly Cipher[] case0OutputCiphers = { Cipher.CaesarCipher, Cipher.AtbashCipher };
    private static readonly Cipher[] case1InputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16 };
    // private static readonly Cipher[] case1OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16 };
    private static readonly Cipher[] case1OutputCiphers = { Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16 };
    private static readonly Cipher[] case2InputCiphers = { Cipher.AtbashCipher };
    // private static readonly Cipher[] case2OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.AtbashCipher };
    private static readonly Cipher[] case2OutputCiphers = { Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.AtbashCipher };
    private static readonly Cipher[] case3InputCiphers = { Cipher.CaesarCipher };
    // private static readonly Cipher[] case3OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.CaesarCipher };
    private static readonly Cipher[] case3OutputCiphers = { Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.CaesarCipher };

    public static EncryptedPassword EncryptPassword(string password, int numberOfEncryptions)
    {
        string newPassword = password;

        List<Cipher> usedCiphersList = new();

        Cipher currentCipher = Cipher.None;

        int currentCase = 0;

        for (int i = 0; i < numberOfEncryptions; i++)
        {
            currentCipher = GetRandomCipher(currentCase, out currentCase, currentCipher);
            usedCiphersList.Add(currentCipher);

            switch (currentCipher)
            {
                case Cipher.ASCII_Base2:
                    newPassword = ASCIIEncryption.Encode(newPassword, 2);
                    break;
                case Cipher.ASCII_Base8:
                    newPassword = ASCIIEncryption.Encode(newPassword, 8);
                    break;
                case Cipher.ASCII_Base10:
                    newPassword = ASCIIEncryption.Encode(newPassword, 10);
                    break;
                case Cipher.ASCII_Base16:
                    newPassword = ASCIIEncryption.Encode(newPassword, 16);
                    break;
                case Cipher.AtbashCipher:
                    newPassword = AtbashCipher.DefaultEncode(newPassword);
                    break;
                case Cipher.CaesarCipher:
                    newPassword = CaesarCipher.DefaultEncode(newPassword);
                    break;
            }
        }

        EncryptedPassword encryptedPassword = new()
        {
            Password = newPassword,
            OriginalPassword = password,
            UsedEncryptions = usedCiphersList.ToArray()
        };

        return encryptedPassword;
    }

    private static Cipher GetRandomCipher(int currentCase, out int newCase, Cipher previousCipher)
    {
        Cipher[] allowedCiphers = new Cipher[0];

        switch (currentCase)
        {
            case 0:
                allowedCiphers = case0OutputCiphers;
                break;
            case 1:
                allowedCiphers = case1OutputCiphers;
                break;
            case 2:
                allowedCiphers = case2OutputCiphers;
                break;
            case 3:
                allowedCiphers = case3OutputCiphers;
                break;
        }

        // May be better to just use a list
        List<Cipher> allowedCiphersList = allowedCiphers.ToList();
        allowedCiphersList.Remove(previousCipher);
        allowedCiphers = allowedCiphersList.ToArray();

        Cipher randomCipher = allowedCiphers[Random.Range(0, allowedCiphers.Length)];

        // If no case is selected, that means that an error occured.
        newCase = -1;

        if (case1InputCiphers.Contains(randomCipher))
        {
            newCase = 1;
        }
        else if (case2InputCiphers.Contains(randomCipher))
        {
            newCase = 2;
        }
        else if (case3InputCiphers.Contains(randomCipher))
        {
            newCase = 3;
        }

        return randomCipher;
    }
}

public class EncryptedPassword
{
    public string Password { get; set; }

    public string OriginalPassword { get; set; }

    public PasswordEncryption.Cipher[] UsedEncryptions { get; set; }
}