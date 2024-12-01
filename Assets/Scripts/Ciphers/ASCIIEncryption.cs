using System;
using UnityEngine;

public static class ASCIIEncryption
{
    public static string Encode(string input, int outputBase)
    {
        string output = "";
        foreach (char c in input)
        {
            output += Convert.ToString(c, outputBase) + " ";
        }
        return output;
    }

    public static string Decode(string input, int inputBase)
    {
        string output = "";
        string encodedCharacter = "";
        foreach (char c in input)
        {
            if (c == ' ')
            {
                output += (TryDecodeCharacter(encodedCharacter, inputBase, out char decodedCharacter)) ? decodedCharacter.ToString() : "Error";
                encodedCharacter = "";
            }
            else
            {
                encodedCharacter += c;
            }
        }
        if (encodedCharacter.Length > 0)
        {
            output += (TryDecodeCharacter(encodedCharacter, inputBase, out char decodedCharacter)) ? decodedCharacter.ToString() : "Error";
        }
        return output;
    }

    private static bool TryDecodeCharacter(string encodedCharacter, int inputBase, out char decodedCharacter)
    {
        try
        {
            int tempInt = Convert.ToInt32(encodedCharacter, inputBase);
            decodedCharacter = (char)tempInt;
            return true;
        }
        catch (Exception)
        {
            decodedCharacter = default;
            return false;
        }
    }
}