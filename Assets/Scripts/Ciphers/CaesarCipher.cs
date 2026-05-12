using System;
using System.Text;

public static class CaesarCipher
{
    public const string DefaultBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int DefaultShift = 13;

    public static string DefaultEncode(string input) => Encode(input, DefaultBase, DefaultShift);

    public static string Encode(string input, string cipherBase, int shift)
    {
        StringBuilder output = new(input.Length);

        foreach (char c in input)
        {
            if (!cipherBase.Contains(c))
            {
                return "Error";
            }

            int index = Math.Abs((cipherBase.IndexOf(c) + shift) % cipherBase.Length);
            output.Append(cipherBase[index]);
        }

        return output.ToString();
    }
}