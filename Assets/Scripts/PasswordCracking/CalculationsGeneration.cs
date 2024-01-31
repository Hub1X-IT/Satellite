using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationsGeneration : MonoBehaviour {

    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TestEncrypting();
        }
    }


    private void TestCalculations() {
        CalculationData.Calculation calculation = CalculationData.Calculation.Multiply;
        int number = 113;

        CalculationData testCalculationData = new CalculationData(number, calculation);
        testCalculationData = GenerateCalculationData(testCalculationData);

        Debug.Log("Calculation: " + calculation);
        Debug.Log("Value1: " + testCalculationData.GetValue1());
        Debug.Log("Value2: " + testCalculationData.GetValue2());
        Debug.Log("Result: " + testCalculationData.GetResult());
    }


    private void TestCalculationsFull() {
        int result = 102;
        CalculationData calculationData0, calculationData1, calculationData2;

        GenerateCalculationsForNumber(result, out calculationData0, out calculationData1, out calculationData2);

        string calculation0 = 
            "Calculation 0\nCalculation: " + calculationData0.GetCalculation() 
            + "\nResult: " + calculationData0.GetResult()
            + "\nValue1: " + calculationData0.GetValue1()
            + "\nValue2: " + calculationData0.GetValue2();
        string calculation1 = 
            "Calculation 1\nCalculation: " + calculationData1.GetCalculation() 
            + "\nResult: " + calculationData1.GetResult()
            + "\nValue1: " + calculationData1.GetValue1() 
            + "\nValue2: " + calculationData1.GetValue2();
        string calculation2 = 
            "Calculation 2\nCalculation: " + calculationData2.GetCalculation() 
            + "\nResult: " + calculationData2.GetResult()
            + "\nValue1: " + calculationData2.GetValue1() 
            + "\nValue2: " + calculationData2.GetValue2();

        Debug.Log(calculation0);
        Debug.Log(calculation1);
        Debug.Log(calculation2);
    }


    private void TestEncrypting() {
        int result = 102;

        EncryptedCharacter encryptedPassword = GetEncryptedPassword(result);

        string debugString = encryptedPassword.GetEncryptedPasswordString() + " = " + encryptedPassword.GetResult();
        Debug.Log(debugString);
    }


    public EncryptedCharacter GetEncryptedPassword(int number) {
        CalculationData calculationDataMiddle, calculationDataFirst, calculationDataLast;
        GenerateCalculationsForNumber(number, out calculationDataMiddle, out calculationDataFirst, out calculationDataLast);

        EncryptedCharacter encryptedPassword = new EncryptedCharacter(calculationDataMiddle, calculationDataFirst, calculationDataLast);

        return encryptedPassword;
    }


    private void GenerateCalculationsForNumber(int number, out CalculationData calculationDataMiddle, out CalculationData calculationDataFirst, out CalculationData calculationDataLast) {
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

        // Calculations 1 & 2
        calculationDataFirst = GenerateCalculation(calculationDataMiddle.GetValue1());
        calculationDataLast = GenerateCalculation(calculationDataMiddle.GetValue2());
    }


    private CalculationData GenerateCalculation(int result) {
        CalculationData calculationData = new CalculationData();
        calculationData.SetResult(result);

        bool multiplicationAllowed = CanAllowMultiplication(calculationData.GetResult());
        calculationData.SetCalculation(GetRandomCalculation(multiplicationAllowed));

        calculationData = GenerateCalculationData(calculationData);

        return calculationData;
    }


    private CalculationData.Calculation GetRandomCalculation(bool multiplicationAllowed) {
        int randomNum;
        if (multiplicationAllowed) {
            randomNum = Random.Range(0, 4);
        }
        else {
            randomNum = Random.Range(0, 3);
        }


        switch (randomNum) {
            case 0:
                return CalculationData.Calculation.Add;
            case 1:
                return CalculationData.Calculation.Subtract;
            case 2:
                return CalculationData.Calculation.Divide;
            case 3:
                return CalculationData.Calculation.Multiply;
            default:
                return CalculationData.Calculation.None;
        }
    }


    private CalculationData GenerateCalculationData(CalculationData calculationData) {
        // Input calculation data must have the result and calculation variables set; otherwise, the function returns null!
        // Returns null when trying to multiply and the result is prime!


        if (calculationData.GetResult() == 0 || calculationData.GetCalculation() == CalculationData.Calculation.None) return null;


        // Add
        if (calculationData.GetCalculation() == CalculationData.Calculation.Add) {
            calculationData.SetValue1(Random.Range(1, calculationData.GetResult()));
            calculationData.SetValue2(calculationData.GetResult() - calculationData.GetValue1());
        }

        // Subtract
        if (calculationData.GetCalculation() == CalculationData.Calculation.Subtract) {
            int rangeMultiplier = 5;
            // Setting a random value (not necessarily a multiply of the rangeMultiplier, also it can't be the result itself - that'a why there are so many calculations)
            calculationData.SetValue1(Random.Range(calculationData.GetResult() + rangeMultiplier, calculationData.GetResult() * rangeMultiplier)
                - Random.Range(0, rangeMultiplier));
            calculationData.SetValue2(calculationData.GetValue1() - calculationData.GetResult());
        }

        // Multiply
        if (calculationData.GetCalculation() == CalculationData.Calculation.Multiply) {
            List<int> divisorsList = GetDivisors(calculationData.GetResult(), out bool isPrime);
            if (isPrime) return null;
            divisorsList.Remove(1);
            divisorsList.Remove(calculationData.GetResult());
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
    

    private List<int> GetDivisors(int number, out bool isPrime) {
        if (number <= 0) {
            isPrime = false;
            return null;
        }

        List<int> divisors = new List<int>();

        for (int i = 1; i < number; i++) {
            if (number % i == 0) {
                divisors.Add(i);
            }
        }

        if (divisors.Count == 1) isPrime = true;
        else isPrime = false;

        divisors.Add(number);
        return divisors;
    }


    private bool CanAllowMultiplication(int number) {
        if (number <= 1) {
            return false;
        }

        for (int i = 2; i < number / 2; i++) {
            if (number % i == 0) {                
                return true;
            }
        }

        return false;
    }


    private int GetRandomNumberFromArray(int[] numbers) {
        return numbers[Random.Range(0, numbers.Length)];
    }
}