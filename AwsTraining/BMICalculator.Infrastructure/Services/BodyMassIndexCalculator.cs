using System;
using BMICalculator.Domain.Models;

namespace BMICalculator.Infrastructure.Services
{
    public class BodyMassIndexCalculator : ICalculator
    {
        public CalculationResult Calculate(byte height, double mass, byte age)
        {
            if (age < 18)
                throw new Exception("BMI calculator can be used for people over 18.");

            var bmi = mass / Math.Pow(height / 100d, 2);

            return new CalculationResult
            {
                BMI = bmi,
                Description = GetIndicationForAdult(bmi).ToString()
            };
        }

        private CalculationDescription GetIndicationForAdult(double bmi)
        {
            if (bmi <= 16)
                return CalculationDescription.ExtremeUnderweight;
            if (bmi > 16 && bmi <= 18.5)
                return CalculationDescription.Underweight;
            if (bmi > 18.5 && bmi <= 25)
                return CalculationDescription.Normal;
            if (bmi > 25 && bmi <= 30)
                return CalculationDescription.Overweight;
            if (bmi > 30 && bmi <= 40)
                return CalculationDescription.Overweight;
            else
                return CalculationDescription.ExtremeObesity;
        }
    }
}
