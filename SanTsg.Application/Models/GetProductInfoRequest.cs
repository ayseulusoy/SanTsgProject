using System;
using System.Collections.Generic;
using System.Text;

namespace SanTsgProject.Application.Models
{
    public class GetProductInfoRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public int productType { get; set; }
            public int ownerProvider { get; set; }
            public string product { get; set; }
            public string culture { get; set; }
        }
    }
}
