using System.Collections.Generic;
using System.Net;

namespace BMICalculator.Domain.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }
        public List<string> Headers => new List<string> { "'Content-Type': 'text/html'" };
    }
}
