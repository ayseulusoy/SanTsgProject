using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Services
{
    internal class BeginTransactionService : IBeginTransactionService
    {
        public Task<string> GetTransactionId(BeginTransactionRequest.Root beginTransactionRequest, string token)
        {
            throw new NotImplementedException();
        }
    }
}
