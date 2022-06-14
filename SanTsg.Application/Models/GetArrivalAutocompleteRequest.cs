using System;
using System.Collections.Generic;
using System.Text;

namespace SanTsgProject.Application.Models
{
    public class GetArrivalAutocompleteRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public int ProductType { get; set; } = 2;
            public string Query { get; set; }
            public string Culture { get; set; } = "en-US";
        }
    }
}
