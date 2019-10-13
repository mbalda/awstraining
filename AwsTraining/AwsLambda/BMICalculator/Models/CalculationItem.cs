using Amazon.DynamoDBv2.DataModel;

namespace BMICalculator.Models
{
    [DynamoDBTable("calculations")]
    public class CalculationItem
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBRangeKey]
        public string Name { get; set; }
        public byte Height { get; set; }
        public double Weight { get; set; }
        public byte Age { get; set; }
        public string Description { get; set; }
        public double BMI{ get; set; }
    }
}
