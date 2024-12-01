public static class CaesarCipher
{
    private const string DefaultBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int DefaultShift = 13;

    public static string DefaultEncode(string input) => Encode(input, DefaultBase, DefaultShift);

    public static string Encode(string input, string cipherBase, int shift)
    {
        string output = string.Empty;

        foreach (char c in input)
        {
            int index = cipherBase.IndexOf(c) + shift;

            while (index >= cipherBase.Length)
            {
                index -= cipherBase.Length;
            }

            output += cipherBase[index];
        }

        return output;
    }
}