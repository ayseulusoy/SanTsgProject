using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string> GetToken(LoginRequest.Root loginRequest);
    }
}
