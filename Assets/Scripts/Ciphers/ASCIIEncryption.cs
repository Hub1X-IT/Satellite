using System;

public static class ASCIIEncryption
{
    public static string Encode(string input, int outputBase)
    {
        string output = "";

        int desiredLength = outputBase switch
        {
            2 => 8,
            8 => 3,
            16 => 2,
            _ => 0,
        };

        string addedString = "";

        /*
        for (int i = 0; i < desiredLength; i++)
        {
            addedString += "0";
        }
        */

        foreach (char c in input)
        {
            output += addedString + Convert.ToString(c, outputBase) + " ";
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