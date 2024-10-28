using System.Collections.Generic;
using UnityEngine;

public class CalculationsGenerationTestCopy : MonoBehaviour
{
    /// Works only for positive numbers!

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestEncrypting();
        }
    }


    private void TestCalculations()
    {
        CalculationData.CalculationType calculation = CalculationData.CalculationType.Multiply;
        int number = 102;

        CalculationData testCalculationData = new CalculationData(number, calculation);
        testCalculationData = GenerateCalculationData(testCalculationData);

        Debug.Log("Calculation: " + calculation);
        Debug.Log("Value1: " + testCalculationData.Value1);
        Debug.Log("Value2: " + testCalculationData.Value2);
        Debug.Log("Result: " + testCalculationData.Result);
    }


    private void TestCalculationsFull()
    {
        int result = 102;
        CalculationData calculationData0, calculationData1, calculationData2;

        GenerateCalculationsForNumber(result, out calculationData0, out calculationData1, out calculationData2);

        string calculation0 =
            "Calculation 0\nCalculation: " + calculationData0.Calculation
            + "\nResult: " + calculationData0.Result
            + "\nValue1: " + calculationData0.Value1
            + "\nValue2: " + calculationData0.Value2;
        string calculation1 =
            "Calculation 1\nCalculation: " + calculationData1.Calculation
            + "\nResult: " + calculationData1.Result
            + "\nValue1: " + calculationData1.Value1
            + "\nValue2: " + calculationData1.Value2;
        string calculation2 =
            "Calculation 2\nCalculation: " + calculationData2.Calculation
            + "\nResult: " + calculationData2.Result
            + "\nValue1: " + calculationData2.Value1
            + "\nValue2: " + calculationData2.Value2;

        Debug.Log(calculation0);
        Debug.Log(calculation1);
        Debug.Log(calculation2);
    }


    private void TestEncrypting()
    {
        int result = 102;

        EncryptedCharacter encryptedPassword = GetEncryptedCharacter(result);

        string debugString = encryptedPassword.EncryptedCharacterString + " = " + encryptedPassword.Result;
        Debug.Log(debugString);
    }


    public static EncryptedCharacter GetEncryptedCharacter(int number)
    {
        GenerateCalculationsForNumber(number, out CalculationData calculationDataMiddle, out CalculationData calculationDataFirst, out CalculationData calculationDataLast);
        return new EncryptedCharacter(calculationDataMiddle, calculationDataFirst, calculationDataLast);
    }


    private static void GenerateCalculationsForNumber(int number, out CalculationData calculationDataMiddle, out CalculationData calculationDataFirst, out CalculationData calculationDataLast)
    {
        /* obsolete
        calculationDataMiddle = new CalculationData();
        calculationDataMiddle.SetResult(number);
        // Calculation 0
        {
            // Randomize between addition and subtraction - the middle calculation
            int randomNum = Random.Range(0, 2);
            if (randomNum == 0) {
                calculationDataMiddle.SetCalculation(CalculationData.Calculation.Add);
            }
            else {
                calculationDataMiddle.SetCalculation(CalculationData.Calculation.Subtract);
            }

            calculationDataMiddle = GenerateCalculationData(calculationDataMiddle);
        }
        */

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
        if (calculationDataMiddle.Calculation == CalculationData.CalculationType.Subtract)
            calculationDataLast = GenerateCalculation(calculationDataMiddle.Value2, false, false, true, true);
        else
            calculationDataLast = GenerateCalculation(calculationDataMiddle.Value2, true, true, true, true);
    }


    private static CalculationData GenerateCalculation(int result, bool additionAllowed, bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed)
    {
        CalculationData calculationData = new()
        {
            Result = result
        };

        multiplicationAllowed = multiplicationAllowed && CanAllowMultiplication(calculationData.Result);
        calculationData.Calculation = GetRandomCalculation(additionAllowed, subtractionAllowed, multiplicationAllowed, divisionAllowed);

        calculationData = GenerateCalculationData(calculationData);

        return calculationData;
    }


    private static CalculationData.CalculationType GetRandomCalculation(bool additionAllowed, bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed)
    {
        List<CalculationData.CalculationType> allowedCalculationsList = new List<CalculationData.CalculationType>();
        if (additionAllowed) allowedCalculationsList.Add(CalculationData.CalculationType.Add);
        if (subtractionAllowed) allowedCalculationsList.Add(CalculationData.CalculationType.Subtract);
        if (multiplicationAllowed) allowedCalculationsList.Add(CalculationData.CalculationType.Multiply);
        if (divisionAllowed) allowedCalculationsList.Add(CalculationData.CalculationType.Divide);

        int randomIndex = Random.Range(0, allowedCalculationsList.Count);
        return allowedCalculationsList.ToArray()[randomIndex];
    }


    private static CalculationData GenerateCalculationData(CalculationData calculationData)
    {
        /// Input calculation data must have the result and calculation variables set; otherwise, the function returns null!
        /// Returns null when trying to multiply and the result is prime!

        if (calculationData.Result == 0 || calculationData.Calculation == CalculationData.CalculationType.None)
        {
            return null;
        }

        switch (calculationData.Calculation)
        {
            case CalculationData.CalculationType.Add:
                // Add
                calculationData.Value1 = Random.Range(1, calculationData.Result);
                calculationData.Value2 = calculationData.Result - calculationData.Value1;
                break;

            case CalculationData.CalculationType.Subtract:
                // Subtract
                int rangeMultiplier = 5;
                calculationData.Value1 = Random.Range(calculationData.Result + 1, calculationData.Result * rangeMultiplier);
                calculationData.Value2 = calculationData.Value1 - calculationData.Result;
                break;

            case CalculationData.CalculationType.Multiply:
                // Multiply
                List<int> divisorsList = GetDivisors(calculationData.Result, out bool isPrime);
                if (isPrime) return null;
                divisorsList.Remove(1);
                divisorsList.Remove(calculationData.Result);
                // calculationData.SetValue1(divisorsList.ToArray()[Random.Range(0, divisorsList.Count)]);
                calculationData.Value1 = GetRandomNumberFromArray(divisorsList.ToArray());
                calculationData.Value2 = calculationData.Result / calculationData.Value1;
                break;

            case CalculationData.CalculationType.Divide:
                // Divide
                int range = 10;
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

        List<int> divisors = new List<int>();
        for (int i = 1; i < number; i++) { if (number % i == 0) divisors.Add(i); }

        if (divisors.Count == 1) isPrime = true;
        else isPrime = false;

        divisors.Add(number);
        return divisors;
    }


    private static bool CanAllowMultiplication(int number)
    {
        if (number <= 1) return false;

        for (int i = 2; i < number / 2; i++) { if (number % i == 0) return true; }

        return false;
    }


    private static int GetRandomNumberFromArray(int[] numbers)
    {
        return numbers[Random.Range(0, numbers.Length)];
    }
}