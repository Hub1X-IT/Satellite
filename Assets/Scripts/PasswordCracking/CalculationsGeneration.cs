using System.Collections.Generic;
using UnityEngine;

public class CalculationsGeneration {

    public static EncryptedCharacter GetEncryptedCharacter(int number) {
        GenerateCalculationsForNumber(number, out CalculationData calculationDataMiddle, out CalculationData calculationDataFirst, out CalculationData calculationDataLast);
        return new EncryptedCharacter(calculationDataMiddle, calculationDataFirst, calculationDataLast);
    }


    private static void GenerateCalculationsForNumber(int number, out CalculationData calculationDataMiddle, out CalculationData calculationDataFirst, out CalculationData calculationDataLast) {
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
        calculationDataFirst = GenerateCalculation(calculationDataMiddle.GetValue1(), true, true, true, true);
        if (calculationDataMiddle.GetCalculation() == CalculationData.Calculation.Subtract) 
            calculationDataLast = GenerateCalculation(calculationDataMiddle.GetValue2(), false, false, true, true);
        else
            calculationDataLast = GenerateCalculation(calculationDataMiddle.GetValue2(), true, true, true, true);
    }


    private static CalculationData GenerateCalculation(int result, bool additionAllowed, bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed) {
        CalculationData calculationData = new CalculationData();
        calculationData.SetResult(result);

        multiplicationAllowed = multiplicationAllowed && CanAllowMultiplication(calculationData.GetResult());
        calculationData.SetCalculation(GetRandomCalculation(additionAllowed, subtractionAllowed, multiplicationAllowed, divisionAllowed));

        calculationData = GenerateCalculationData(calculationData);

        return calculationData;
    }


    private static CalculationData.Calculation GetRandomCalculation(bool additionAllowed, bool subtractionAllowed, bool multiplicationAllowed, bool divisionAllowed) {
        List<CalculationData.Calculation> allowedCalculationsList = new List<CalculationData.Calculation>();
        if (additionAllowed) allowedCalculationsList.Add(CalculationData.Calculation.Add);
        if (subtractionAllowed) allowedCalculationsList.Add(CalculationData.Calculation.Subtract);
        if (multiplicationAllowed) allowedCalculationsList.Add(CalculationData.Calculation.Multiply);
        if (divisionAllowed) allowedCalculationsList.Add(CalculationData.Calculation.Divide);
        
        int randomIndex = Random.Range(0, allowedCalculationsList.Count);
        return allowedCalculationsList.ToArray()[randomIndex];
    }


    private static CalculationData GenerateCalculationData(CalculationData calculationData) {
        /// Input calculation data must have the result and calculation variables set; otherwise, the function returns null!
        /// Returns null when trying to multiply and the result is prime!


        if (calculationData.GetResult() == 0 || calculationData.GetCalculation() == CalculationData.Calculation.None) return null;


        // Add
        if (calculationData.GetCalculation() == CalculationData.Calculation.Add) {
            calculationData.SetValue1(Random.Range(1, calculationData.GetResult()));
            calculationData.SetValue2(calculationData.GetResult() - calculationData.GetValue1());
        }

        // Subtract
        if (calculationData.GetCalculation() == CalculationData.Calculation.Subtract) {
            int rangeMultiplier = 5;                        
            calculationData.SetValue1(Random.Range(calculationData.GetResult() + 1, calculationData.GetResult() * rangeMultiplier));
            calculationData.SetValue2(calculationData.GetValue1() - calculationData.GetResult());
        }

        // Multiply
        if (calculationData.GetCalculation() == CalculationData.Calculation.Multiply) {
            List<int> divisorsList = GetDivisors(calculationData.GetResult(), out bool isPrime);
            if (isPrime) return null;
            divisorsList.Remove(1);
            divisorsList.Remove(calculationData.GetResult());
            // calculationData.SetValue1(divisorsList.ToArray()[Random.Range(0, divisorsList.Count)]);
            calculationData.SetValue1(GetRandomNumberFromArray(divisorsList.ToArray()));
            calculationData.SetValue2(calculationData.GetResult() / calculationData.GetValue1());
        }

        // Divide
        if (calculationData.GetCalculation() == CalculationData.Calculation.Divide) {
            int range = 10;
            calculationData.SetValue2(Random.Range(2, range));
            calculationData.SetValue1(calculationData.GetValue2() * calculationData.GetResult());
        }
        
        return calculationData;
    }
    

    private static List<int> GetDivisors(int number, out bool isPrime) {
        if (number <= 0) {
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

    private static bool CanAllowMultiplication(int number) {
        if (number <= 1) return false;

        for (int i = 2; i < number / 2; i++) { if (number % i == 0) return true; }

        return false;
    }

    private static int GetRandomNumberFromArray(int[] numbers) {
        return numbers[Random.Range(0, numbers.Length)];
    }
}