public static class CaesarCipher
{
    public const string DefaultBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int DefaultShift = 13;

    public static string DefaultEncode(string input) => Encode(input, DefaultBase, DefaultShift);

    public static string Encode(string input, string cipherBase, int shift)
    {
        string output = string.Empty;

        foreach (char c in input)
        {
            if (!cipherBase.Contains(c))
            {
                output += "Error";
                continue;
            }

            int index = cipherBase.IndexOf(c) + shift;

            while (index >= cipherBase.Length)
            {
                index -= cipherBase.Length;
            }
            while (index < 0)
            {
                index += cipherBase.Length;
            }

            output += cipherBase[index];
        }

        return output;
    }
}