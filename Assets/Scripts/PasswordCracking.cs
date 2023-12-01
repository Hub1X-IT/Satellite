using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCracking : MonoBehaviour {


    private enum Calculation {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
    }


    private Calculation nextCalculation;


    private void Start() {



    }


    private void GenerateCalculations() {

    }


    private Calculation GenerateNextCalculation() {
        // Generate a random calculation
        Array values = Enum.GetValues(typeof(Calculation));
        System.Random random = new System.Random();
        Calculation randomCalculation = (Calculation)values.GetValue(random.Next(values.Length));
        return randomCalculation;
    }


    private int GenerateRandomIndex(int minVal, int maxVal) {
        return UnityEngine.Random.Range(minVal, maxVal);
    }


    private int[] GetDivisors(int number, out bool isPrime) {
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
        return divisors.ToArray();
    }

}