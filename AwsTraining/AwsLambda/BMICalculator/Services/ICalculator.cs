using BMICalculator.Models;

namespace BMICalculator.Services
{
    public interface ICalculator
    {
        CalculationResult Calculate(byte height, double mass, byte age);
    }
}