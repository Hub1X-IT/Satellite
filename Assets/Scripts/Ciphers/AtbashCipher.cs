using System.Text;

public static class AtbashCipher
{
    private const string DefaultBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string DefaultEncode(string input) => Encode(input, DefaultBase);

    public static string Encode(string input, string cipherBase)
    {
        StringBuilder output = new(input.Length);

        foreach (char c in input)
        {
            if (!cipherBase.Contains(c))
            {
                return "Error";
            }

            int index = cipherBase.Length - cipherBase.IndexOf(c) - 1;
            output.Append(cipherBase[index]);
        }

        return output.ToString();
    }
}