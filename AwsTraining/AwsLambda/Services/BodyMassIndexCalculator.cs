using System;
using System.Collections.Generic;
using System.Text;
using AwsLambda.Models;

namespace AwsLambda.Services
{
    public class BodyMassIndexCalculator
    {
        public WeightInformation Calculate(byte height, double mass)
        {
            var bmi = mass / ((height / 100) * (height / 100));

            if (bmi <= 16)
                return WeightInformation.ExtremeUnderweight;
            if (bmi > 16 && bmi <= 17)
                return WeightInformation.Underweight;
            if (bmi > 17 && bmi <= 25)
                return WeightInformation.Normal;
            if (bmi > 25 && bmi <= 30)
                return WeightInformation.Obesity;
            else
                return WeightInformation.ExtremeObesity;
        }
    }
}
