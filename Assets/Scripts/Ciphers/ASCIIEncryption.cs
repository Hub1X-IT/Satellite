using System;
using System.Text;

public static class ASCIIEncryption
{
    private const string allowedCharacters = " 1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string Encode(string input, int outputBase)
    {
        StringBuilder output = new();
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
            output.Append(convertedNumber.PadLeft(desiredLength, '0'));
            output.Append(" ");
        }

        return output.ToString();
    }

    public static string Decode(string input, int inputBase)
    {
        StringBuilder output = new();
        foreach (var item in input.Split(' '))
        {
            if (TryDecodeCharacter(item, inputBase, out char decodedCharacter) && allowedCharacters.Contains(decodedCharacter))
            {
                output.Append(decodedCharacter);
            }
            else
            {
                return "Error";
            }
        }

        return output.ToString();
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