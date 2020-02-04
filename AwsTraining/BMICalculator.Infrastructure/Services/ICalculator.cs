using BMICalculator.Domain.Models;

namespace BMICalculator.Infrastructure.Services
{
    public interface ICalculator
    {
        CalculationResult Calculate(byte height, double mass, byte age);
    }
}