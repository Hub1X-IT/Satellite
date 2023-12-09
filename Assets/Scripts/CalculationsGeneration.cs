using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationsGeneration : MonoBehaviour {
    

    private CalculationData calculationData1 = new CalculationData();
    private CalculationData calculationData2 = new CalculationData();
    private CalculationData calculationData3 = new CalculationData();
    private CalculationData calculationData4 = new CalculationData();


    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            TestCalculations();
        }
    }


    private void TestCalculations() {
        CalculationData.Calculation calculation = CalculationData.Calculation.Divide;
        int number = 102;
        CalculationData data = GenerateNextCalculation(calculation, number);
        Debug.Log("Calculation: " + calculation);
        Debug.Log("Value1: " + data.GetValue1());
        Debug.Log("Value2: " + data.GetValue2());
        Debug.Log("Result: " + data.GetResult());
    }
        

    public void GenerateCalculations(CalculationData calculationData) {
        
    }


    private CalculationData GenerateNextCalculation(CalculationData.Calculation calculation, int number) {
        // Returns null when trying to multiply and the number is prime!

        CalculationData calculationData = new CalculationData();
        calculationData.SetCalculation(calculation);
        calculationData.SetResult(number);


        if (calculation == CalculationData.Calculation.Add) {
            calculationData.SetValue1(UnityEngine.Random.Range(1, calculationData.GetResult()));
            calculationData.SetValue2(calculationData.GetResult() - calculationData.GetValue1());
        }
        else if (calculation == CalculationData.Calculation.Subtract) {
            int rangeMultiplier = 5;
            calculationData.SetValue1(UnityEngine.Random.Range(calculationData.GetResult(), calculationData.GetResult() * rangeMultiplier)
                - UnityEngine.Random.Range(0, rangeMultiplier));
            calculationData.SetValue2(calculationData.GetValue1() - calculationData.GetResult());
        }
        else if (calculation == CalculationData.Calculation.Multiply) {
            List<int> divisorsList = GetDivisors(number, out bool isPrime);
            if (isPrime) return null;
            divisorsList.Remove(1);
            divisorsList.Remove(number);
            calculationData.SetValue1(GetRandomNumber(divisorsList.ToArray()));
            calculationData.SetValue2(calculationData.GetResult() / calculationData.GetValue1());
        }
        else if (calculation == CalculationData.Calculation.Divide) {
            int range = 10;
            calculationData.SetValue2(UnityEngine.Random.Range(2, range));
            calculationData.SetValue1(calculationData.GetValue2() * calculationData.GetResult());
        }
        
        return calculationData;

    }


    private CalculationData.Calculation GenerateRandomCalculation() {
        // Generate a random calculation
        Array values = Enum.GetValues(typeof(CalculationData.Calculation));
        System.Random random = new System.Random();
        CalculationData.Calculation randomCalculation = (CalculationData.Calculation)values.GetValue(random.Next(values.Length));
        return randomCalculation;
    }


    private List<int> GetDivisors(int number, out bool isPrime) {
        if (number == 0) {
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


    private int GetRandomNumber(int[] numbers) {
        return numbers[UnityEngine.Random.Range(0, numbers.Length)];
    }
}