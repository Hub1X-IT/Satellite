using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PasswordEncryption
{
    public enum CipherType
    {
        None,
        ASCII_Base2,
        ASCII_Base8,
        ASCII_Base10,
        ASCII_Base16,
        AtbashCipher,
        CaesarCipher,
    }

    public class EncryptionStep
    {
        public string PreviousPasswordState;
        public string CurrentPasswordState;
        public CipherType usedCipherType;
    }

    
    /*
        Case:
        0: No ciphers used, every cipher allowed.
        1: An ASCII cipher was used: the next cipher has to be one of the ASCII ciphers.
        2: Atbash cipher was used, the next one can't be Caesar.
        3: Caesar cipher was used, the next one can't be Atbash.
    */

    /*
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
    */


    /*
        Case:
        0: No ciphers used, every cipher allowed.
        1: An ASCII cipher was used: the next cipher has to be one of the ASCII ciphers.
        2: Atbash cipher was used, every cipher allowed.
    */

    // private static readonly Cipher[] case0OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.AtbashCipher };
    private static readonly CipherType[] case0OutputCiphers = { CipherType.ASCII_Base8, CipherType.ASCII_Base10, CipherType.ASCII_Base16, CipherType.AtbashCipher };
    // private static readonly Cipher[] case0OutputCiphers = { Cipher.CaesarCipher, Cipher.AtbashCipher };
    private static readonly CipherType[] case1InputCiphers = { CipherType.ASCII_Base2, CipherType.ASCII_Base8, CipherType.ASCII_Base10, CipherType.ASCII_Base16 };
    // private static readonly Cipher[] case1OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16 };
    private static readonly CipherType[] case1OutputCiphers = { CipherType.ASCII_Base8, CipherType.ASCII_Base10, CipherType.ASCII_Base16 };
    private static readonly CipherType[] case2InputCiphers = { CipherType.AtbashCipher };
    // private static readonly Cipher[] case2OutputCiphers = { Cipher.ASCII_Base2, Cipher.ASCII_Base8, Cipher.ASCII_Base10, Cipher.ASCII_Base16, Cipher.AtbashCipher };
    private static readonly CipherType[] case2OutputCiphers = { CipherType.ASCII_Base8, CipherType.ASCII_Base10, CipherType.ASCII_Base16, CipherType.AtbashCipher };


    public static EncryptedPassword EncryptPassword(string password, int numberOfEncryptions)
    {
        string newPassword = password;

        List<CipherType> usedCiphersList = new();

        CipherType currentCipher = CipherType.None;

        int currentCase = 0;

        List<EncryptionStep> encryptionSteps = new();

        for (int i = 0; i < numberOfEncryptions; i++)
        {
            currentCipher = GetRandomCipher(currentCase, out currentCase, currentCipher);
            usedCiphersList.Add(currentCipher);

            EncryptionStep currentEncryptionStep = new()
            {
                PreviousPasswordState = newPassword,
                usedCipherType = currentCipher
            };

            switch (currentCipher)
            {
                case CipherType.ASCII_Base2:
                    newPassword = ASCIIEncryption.Encode(newPassword, 2);
                    break;
                case CipherType.ASCII_Base8:
                    newPassword = ASCIIEncryption.Encode(newPassword, 8);
                    break;
                case CipherType.ASCII_Base10:
                    newPassword = ASCIIEncryption.Encode(newPassword, 10);
                    break;
                case CipherType.ASCII_Base16:
                    newPassword = ASCIIEncryption.Encode(newPassword, 16);
                    break;
                case CipherType.AtbashCipher:
                    newPassword = AtbashCipher.DefaultEncode(newPassword);
                    break;
                case CipherType.CaesarCipher:
                    newPassword = CaesarCipher.DefaultEncode(newPassword);
                    break;
            }

            currentEncryptionStep.CurrentPasswordState = newPassword;
            encryptionSteps.Add(currentEncryptionStep);
        }

        EncryptedPassword encryptedPassword = new()
        {
            Password = newPassword,
            OriginalPassword = password,
            UsedEncryptions = usedCiphersList.ToArray(),
            EncryptionSteps = encryptionSteps.ToArray()
        };

        return encryptedPassword;
    }

    private static CipherType GetRandomCipher(int currentCase, out int newCase, CipherType previousCipher)
    {
        CipherType[] allowedCiphers = new CipherType[0];

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
            /*
            case 3:
                allowedCiphers = case3OutputCiphers;
                break;
            */
        }

        // May be better to just use a list
        List<CipherType> allowedCiphersList = allowedCiphers.ToList();
        allowedCiphersList.Remove(previousCipher);
        allowedCiphers = allowedCiphersList.ToArray();

        CipherType randomCipher = allowedCiphers[Random.Range(0, allowedCiphers.Length)];

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
        /*
        else if (case3InputCiphers.Contains(randomCipher))
        {
            newCase = 3;
        }
        */

        return randomCipher;
    }
}
