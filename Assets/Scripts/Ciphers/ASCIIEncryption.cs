using System;

public static class ASCIIEncryption
{
    private const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890";

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

        foreach (char c in input)
        {
            string convertedNumber = Convert.ToString(c, outputBase);

            string addedString = "";

            for (int i = 0; i < desiredLength - convertedNumber.Length; i++)
            {
                addedString += "0";
            }

            output += addedString + convertedNumber + " ";
        }

        return output;
    }

    public static string Decode(string input, int inputBase)
    {
        string output = "";
        string encodedCharacter = "";

        for (int i = 0; i < input.Length + 1; i++)
        {
            if (i >= input.Length || input[i] == ' ')
            {
                if (encodedCharacter.Length > 0)
                {
                    if (TryDecodeCharacter(encodedCharacter, inputBase, out char decodedCharacter) && allowedCharacters.Contains(decodedCharacter))
                    {
                        output += decodedCharacter;
                        encodedCharacter = "";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
            else
            {
                encodedCharacter += input[i];
            }
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