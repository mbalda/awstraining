using BMICalculator.Models;
using System.Threading.Tasks;

namespace BMICalculator.Services
{
    internal interface IStore
    {
        Task StoreAsync(CalculationItem item);
        Task<CalculationItem> GetItemByIdAsync(int itemId);
    }
}