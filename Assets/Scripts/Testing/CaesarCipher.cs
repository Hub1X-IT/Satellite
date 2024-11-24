public static class CaesarCipher
{
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