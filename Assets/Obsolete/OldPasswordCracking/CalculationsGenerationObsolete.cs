using System.Collections.Generic;
using UnityEngine;

public static class CalculationsGenerationObsolete
{
    private const int SubtractRangeMultiplier = 2;
    private const int DivideRange = 10;

    // Not implemented yet!
    private const int MaxNumberDivided = 100;

    // Division temporarily can't be used because it generates too large numbers.

    public static EncryptedCharacterObsolete GetEncryptedCharacterForNumber(int number)
    {
        GenerateCalculationsForNumber(number, out var calculationDataMiddle, out var calculationDataFirst, out var calculationDataLast);
        return new EncryptedCharacterObsolete(calculationDataMiddle, calculationDataFirst, calculationDataLast);
    }

    private static void GenerateCalculationsForNumber(int number, out CalculationDataObsolete calculationDataMiddle, out CalculationDataObsolete calculationDataFirst, out CalculationDataObsolete calculationDataLast)
    {
        // Calculation 0 (middle calculation)
        /*
            Only addition and subtraction are allowed for this calculation.
        */
        calculationDataMiddle = GenerateCalculation(number, true, true, false, false);

        // Calculations 1 & 2
        /*
            If the middle calculation is subtraction, the second calculation would need brackets (if it's addition or subtraction),
            so to get rid of this problem, if the middle calculation is subtraction, the second has to be multiplication or division.
        */
        calculationDataFirst = GenerateCalculation(calculationDataMiddle.Value1, true, true, true, true);
        if (calculationDataMiddle.Calculation == CalculationDataObsolete.CalculationType.Subtract)
        {
            calculationDataLast = GenerateCalculation(calculationDataMiddle.Value2, false, false, true, true);
        }
        else
        {
            calculationDataLast = GenerateCalculation(calculationDataMiddle.Value2, true, true, true, true);
        }
    }


    private static CalculationDataObsolete GenerateCalculation(int result, bool additionAllowed, 
        bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed)
    {
        CalculationDataObsolete calculationData = new()
        {
            Result = result
        };

        multiplicationAllowed = multiplicationAllowed && CanAllowMultiplication(calculationData.Result);

        divisionAllowed = divisionAllowed && result <= MaxNumberDivided;

        calculationData.Calculation = GetRandomCalculation(additionAllowed, subtractionAllowed, multiplicationAllowed, divisionAllowed);

        calculationData = GenerateCalculationData(calculationData);

        return calculationData;
    }


    private static CalculationDataObsolete.CalculationType GetRandomCalculation(bool additionAllowed, 
        bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed)
    {
        List<CalculationDataObsolete.CalculationType> allowedCalculationsList = new();
        if (additionAllowed) allowedCalculationsList.Add(CalculationDataObsolete.CalculationType.Add);
        if (subtractionAllowed) allowedCalculationsList.Add(CalculationDataObsolete.CalculationType.Subtract);
        if (multiplicationAllowed) allowedCalculationsList.Add(CalculationDataObsolete.CalculationType.Multiply);
        
        /*
        if (divisionAllowed) allowedCalculationsList.Add(CalculationData.CalculationType.Divide);
        */

        int randomIndex = Random.Range(0, allowedCalculationsList.Count - 1);

        return allowedCalculationsList.ToArray()[randomIndex];
    }


    private static CalculationDataObsolete GenerateCalculationData(CalculationDataObsolete calculationData)
    {
        /// Input calculation data must have the result and calculation variables set; otherwise, the function returns null!
        /// Returns null when trying to multiply and the result is prime!

        if (calculationData.Result == 0 || calculationData.Calculation == CalculationDataObsolete.CalculationType.None)
        {
            return null;
        }

        switch (calculationData.Calculation)
        {
            case CalculationDataObsolete.CalculationType.Add:
                // Add
                calculationData.Value1 = Random.Range(1, calculationData.Result);
                calculationData.Value2 = calculationData.Result - calculationData.Value1;
                break;

            case CalculationDataObsolete.CalculationType.Subtract:
                // Subtract
                int rangeMultiplier = SubtractRangeMultiplier;
                calculationData.Value1 = Random.Range(calculationData.Result + 1, calculationData.Result * rangeMultiplier);
                calculationData.Value2 = calculationData.Value1 - calculationData.Result;
                break;

            case CalculationDataObsolete.CalculationType.Multiply:
                // Multiply
                List<int> divisorsList = GetDivisors(calculationData.Result, out bool isPrime);
                if (isPrime) return null;
                divisorsList.Remove(1);
                divisorsList.Remove(calculationData.Result);
                // calculationData.SetValue1(divisorsList.ToArray()[Random.Range(0, divisorsList.Count)]);
                calculationData.Value1 = GetRandomValueFromArray(divisorsList.ToArray());
                calculationData.Value2 = calculationData.Result / calculationData.Value1;
                break;

            case CalculationDataObsolete.CalculationType.Divide:
                // Divide
                int range = DivideRange;
                calculationData.Value2 = Random.Range(2, range);
                calculationData.Value1 = calculationData.Value2 * calculationData.Result;
                break;
        }

        return calculationData;
    }


    private static List<int> GetDivisors(int number, out bool isPrime)
    {
        if (number <= 0)
        {
            isPrime = false;
            return null;
        }

        List<int> divisors = new();
        for (int i = 1; i < number; i++)
        {
            if (number % i == 0)
            {
                divisors.Add(i);
            }
        }

        if (divisors.Count == 1)
        {
            isPrime = true;
        }
        else
        {
            isPrime = false;
        }

        divisors.Add(number);
        return divisors;
    }

    private static bool CanAllowMultiplication(int number)
    {
        if (number <= 1)
        {
            return false;
        }

        for (int i = 2; i < number / 2; i++)
        {
            if (number % i == 0)
            {
                return true;
            }
        }

        return false;
    }

    private static T GetRandomValueFromArray<T>(T[] values)
    {
        return values[Random.Range(0, values.Length - 1)];
    }
}