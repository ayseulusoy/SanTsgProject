using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{
    internal interface IBeginTransactionService
    {
        Task<string> GetTransactionId(BeginTransactionRequest.Root beginTransactionRequest,string token);
    }
}
