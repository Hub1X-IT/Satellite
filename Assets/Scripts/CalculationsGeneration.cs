using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationsGeneration : MonoBehaviour {

    /*
    private CalculationData calculationData0 = new CalculationData();
    private CalculationData calculationData1 = new CalculationData();
    private CalculationData calculationData2 = new CalculationData();
    */


    private void Start() {
        Debug.Log(IsPrime(1));
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TestCalculationsFull();
        }
    }


    private void TestCalculations() {
        CalculationData.Calculation calculation = CalculationData.Calculation.Multiply;
        int number = 102;

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

        GenerateAllCalculations(result, out calculationData0, out calculationData1, out calculationData2);

        string calculation0 = "Calculation 0\nCalculation: " + calculationData0.GetCalculation() + "\nResult: " + calculationData0.GetResult()
            + "\nValue1: " + calculationData0.GetValue1() + "\nValue2: " + calculationData0.GetValue2();
        string calculation1 = "Calculation 1\nCalculation: " + calculationData1.GetCalculation() + "\nResult: " + calculationData1.GetResult()
            + "\nValue1: " + calculationData1.GetValue1() + "\nValue2: " + calculationData1.GetValue2();
        string calculation2 = "Calculation 2\nCalculation: " + calculationData2.GetCalculation() + "\nResult: " + calculationData2.GetResult()
            + "\nValue1: " + calculationData2.GetValue1() + "\nValue2: " + calculationData2.GetValue2();

        Debug.Log(calculation0);
        Debug.Log(calculation1);
        Debug.Log(calculation2);
    }

    
    public void GenerateAllCalculations(int number, out CalculationData calculationData0, out CalculationData calculationData1, out CalculationData calculationData2) {
        calculationData0 = new CalculationData();
        calculationData0.SetResult(number);
        // Calculation 0
        {
            // Randomise between addition and subtraction - the middle calculation
            int randomNum = UnityEngine.Random.Range(0, 2);
            if (randomNum == 0) {
                calculationData0.SetCalculation(CalculationData.Calculation.Add);
            }
            else {
                calculationData0.SetCalculation(CalculationData.Calculation.Subtract);
            }

            calculationData0 = GenerateCalculationData(calculationData0);
        }

        // Calculations 1 & 2
        calculationData1 = GenerateCalculation(calculationData0.GetValue1());
        calculationData2 = GenerateCalculation(calculationData0.GetValue2());
    }
    

    private CalculationData GenerateCalculationData(CalculationData calculationData) {
        // Returns null when trying to multiply and the number is prime!
        // Input calculation data must have the variables: result and calculation set; otherwise, the function returns null!

        if (calculationData.GetResult() == 0 || calculationData.GetCalculation() == CalculationData.Calculation.None) return null;

        
        if (calculationData.GetCalculation() == CalculationData.Calculation.Add) {
            calculationData.SetValue1(UnityEngine.Random.Range(1, calculationData.GetResult()));
            calculationData.SetValue2(calculationData.GetResult() - calculationData.GetValue1());
        }
        else if (calculationData.GetCalculation() == CalculationData.Calculation.Subtract) {
            int rangeMultiplier = 5;
            calculationData.SetValue1(UnityEngine.Random.Range(calculationData.GetResult(), calculationData.GetResult() * rangeMultiplier)
                - UnityEngine.Random.Range(0, rangeMultiplier));
            calculationData.SetValue2(calculationData.GetValue1() - calculationData.GetResult());
        }
        else if (calculationData.GetCalculation() == CalculationData.Calculation.Multiply) {
            List<int> divisorsList = GetDivisors(calculationData.GetResult(), out bool isPrime);
            if (isPrime) return null;
            divisorsList.Remove(1);
            divisorsList.Remove(calculationData.GetResult());
            calculationData.SetValue1(GetRandomNumber(divisorsList.ToArray()));
            calculationData.SetValue2(calculationData.GetResult() / calculationData.GetValue1());
        }
        else if (calculationData.GetCalculation() == CalculationData.Calculation.Divide) {
            int range = 10;
            calculationData.SetValue2(UnityEngine.Random.Range(2, range));
            calculationData.SetValue1(calculationData.GetValue2() * calculationData.GetResult());
        }
        
        return calculationData;

    }


    private CalculationData GenerateCalculation(int result) {
        CalculationData calculationData = new CalculationData();
        calculationData.SetResult(result);

        bool multiplicationAllowed = !IsPrime(calculationData.GetResult());
        calculationData.SetCalculation(GetRandomCalculation(multiplicationAllowed));

        calculationData = GenerateCalculationData(calculationData);

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


    private bool IsPrime(int number) {
        if (number <= 1) {
            return false;
        }

        for (int i = 1; i < number; i++) {
            if (number % i == 0) {
                return false;
            }
        }
        return true;
    }


    private int GetRandomNumber(int[] numbers) {
        return numbers[UnityEngine.Random.Range(0, numbers.Length)];
    }


    private CalculationData.Calculation GetRandomCalculation(bool multiplicationAllowed) {
        int randomNum = -1;
        if (multiplicationAllowed) {
            randomNum = UnityEngine.Random.Range(0, 4);
        }
        else {
            randomNum = UnityEngine.Random.Range(0, 3);
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
}