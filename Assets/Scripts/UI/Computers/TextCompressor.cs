using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class TextCompressor
{
    private static Dictionary<string, string> uncompressedTextDictionary = new();
    private static Dictionary<string, string> compressedTextDictionary = new();

    private const string AllowedKeyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const int CompressedTextLength = 8;

    public static void InitializeTextCompressor()
    {
        uncompressedTextDictionary = new();
        compressedTextDictionary = new();
    }

    public static string GetCompressedText(string uncompressedText)
    {
        string compressedText;

        if (compressedTextDictionary.ContainsKey(uncompressedText))
        {
            compressedText = compressedTextDictionary[uncompressedText];
        }
        else
        {
            do
            {
                compressedText = GenerateKey(CompressedTextLength);
            }
            while (uncompressedTextDictionary.ContainsKey(compressedText));

            uncompressedTextDictionary.Add(compressedText, uncompressedText);
            compressedTextDictionary.Add(uncompressedText, compressedText);
        }

        return compressedText;
    }

    public static bool TryGetDecompressedText(string compressedText, out string uncompressedText)
    {
        if (uncompressedTextDictionary.ContainsKey(compressedText))
        {
            uncompressedText = uncompressedTextDictionary[compressedText];
            return true;
        }
        else
        {
            uncompressedText = null;
            return false;
        }
    }

    private static string GenerateKey(int length)
    {
        StringBuilder outputKey = new(length);
        for (int i = 0; i < length; i++)
        {
            outputKey.Append(AllowedKeyChars[Random.Range(0, AllowedKeyChars.Length)]);
        }
        return outputKey.ToString();
    }
}
