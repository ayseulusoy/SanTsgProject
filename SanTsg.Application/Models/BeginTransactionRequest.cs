using System;
using System.Collections.Generic;
using System.Text;

namespace SanTsgProject.Application.Models
{
    public class BeginTransactionRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public List<string> offerIds { get; set; }
            public string currency { get; set; }
            public string culture { get; set; }
        }


    }
}
