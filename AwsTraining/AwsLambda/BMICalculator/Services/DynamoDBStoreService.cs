using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BMICalculator.Models;

namespace BMICalculator.Services
{
    class DynamoDbStoreService : IStore
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public DynamoDbStoreService()
        {
            _client = new AmazonDynamoDBClient();
            _context = new DynamoDBContext(_client);
        }

        public async Task<CalculationItem> GetItemByIdAsync(int itemId)
        {
            return await _context.LoadAsync<CalculationItem>(itemId);
        }

        public async Task StoreAsync(CalculationItem item)
        {
            if(item != null)
                await _context.SaveAsync(item);
        }
    }
}
