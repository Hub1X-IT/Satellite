using System.Collections.Generic;
using UnityEngine;

public class AsciiCodingObsolete : MonoBehaviour
{
    public class CodedPassword
    {
        public string password;
        public int[] asciiNumbers;
    }


    public static CodedPassword EncodePassword(string password)
    {
        CodedPassword codedPassword = new CodedPassword();
        codedPassword.password = password;
        codedPassword.asciiNumbers = EncodeToAscii(password);
        return codedPassword;
    }


    private static int[] EncodeToAscii(string password)
    {
        List<int> intList = new List<int>();
        foreach (int i in password) { intList.Add(i); }
        return intList.ToArray();
    }
}