using BMICalculator.Models;

namespace BMICalculator.Services
{
    public class BodyMassIndexCalculator
    {
        public WeightInformation Calculate(byte height, double mass, byte age)
        {
            var bmi = mass / ((height / 100) * (height / 100));

            return age < 18 ? GetIndicationForChild(bmi) : GetIndicationForAdult(bmi);

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

        private WeightInformation GetIndicationForChild(double bmi)
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
