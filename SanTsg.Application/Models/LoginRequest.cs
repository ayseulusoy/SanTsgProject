using System;
using System.Collections.Generic;
using System.Text;

namespace SanTsgProject.Application.Models
{
    public class LoginRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            public string Agency { get; set; } = "PXM25397";
            public string User { get; set; } = "USR1";
            public string Password { get; set; } = "test!23";
        }
    }
}
