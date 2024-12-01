public static class AtbashCipher
{
    private const string DefaultBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string DefaultEncode(string input) => Encode(input, DefaultBase);

    public static string Encode(string input, string cipherBase)
    {
        string output = string.Empty;

        foreach (char c in input)
        {
            int index = cipherBase.Length - cipherBase.IndexOf(c) - 1;
            output += cipherBase[index];
        }

        return output;
    }
}