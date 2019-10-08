using BMICalculator.Models;
using System;

namespace BMICalculator.Services
{
    public class BodyMassIndexCalculator
    {
        public WeightInformation Calculate(byte height, double mass, byte age)
        {
            if (age < 18)
                throw new Exception("BMI calculator can be used for people over 18.");

            var bmi = mass / Math.Pow(height / 100d, 2);
            return GetIndicationForAdult(bmi);
        }

        private WeightInformation GetIndicationForAdult(double bmi)
        {
            if (bmi <= 16)
                return WeightInformation.ExtremeUnderweight;
            if (bmi > 16 && bmi <= 18.5)
                return WeightInformation.Underweight;
            if (bmi > 18.5 && bmi <= 25)
                return WeightInformation.Normal;
            if (bmi > 25 && bmi <= 30)
                return WeightInformation.Obesity;
            else
                return WeightInformation.ExtremeObesity;
        }
    }
}
