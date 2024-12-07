using System;
using System.Collections.Generic;
using UnityEngine;

public static class EncryptedPasswordCompressorNotWorking
{
    private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static int convertBase = characters.Length;

    private static string ConvertToCharactersBase(long number)
    {
        string output = "";

        if (number == 0)
        {
            Debug.Log("0");
            return "0";
        }

        while (number > 0)
        {
            output = output.Insert(0, characters[(int)(number % convertBase)].ToString());
            number /= convertBase;
        }

        return output;
    }

    private static long ConvertFromCharactersBase(string input)
    {
        long number = 0;

        foreach (char c in input)
        {
            number = convertBase * number + characters.IndexOf(c);
        }

        return number;
    }

    public static string Compress(string password)
    {
        string tempString = "";

        foreach (char c in password)
        {
            tempString += Convert.ToString(c, 16);
        }

        Debug.Log(tempString);

        List<long> longsList = new List<long>();

        /*
        int length = 1;
        
        while (false)
        {
            try
            {
                long outputLong = Convert.ToInt64(tempString.Substring(0, length), 16);
                longsList[longsList.Count - 1] = outputLong;
                length++;

                if (length >= tempString.Length)
                {
                    break;
                }
            }
            catch (Exception)
            {
                longsList.Add(0);

                if (tempString.Length <= length)
                {
                    break;
                }

                tempString = tempString.Substring(length, tempString.Length - length);
                length = 0;
            }
        }
        */

        foreach (var l in longsList)
        {
            Debug.Log(l);
        }

        long tempLong = 1;
        
        string output = ConvertToCharactersBase(tempLong);

        return output;
    }

    public static string Decompress(string password)
    {
        long tempInt = ConvertFromCharactersBase(password);

        string tempString = tempInt.ToString();

        string output = "";

        for (int i = 0; i < password.Length; i += 2)
        {
            output += (char)(Convert.ToInt32(tempString.Substring(i, 2), 16));
        }

        return output;
    }

}