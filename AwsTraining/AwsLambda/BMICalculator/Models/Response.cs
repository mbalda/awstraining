using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BMICalculator.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }
        public List<string> Headers => new List<string> { "'Content-Type': 'text/html'" };
    }
}
