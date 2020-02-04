using System.Threading.Tasks;
using BMICalculator.Domain.Models;

namespace BMICalculator.Infrastructure.Services
{
    public interface IStore
    {
        Task StoreAsync(CalculationItem item);
        Task<CalculationItem> GetItemByIdAsync(string itemId);
    }
}