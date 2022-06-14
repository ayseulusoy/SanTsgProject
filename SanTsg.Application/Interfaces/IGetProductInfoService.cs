using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{
    public interface IGetProductInfoService
    {
        Task<GetProductInfoResponse.Hotel> GetDetails(string poductId, string token);
    }
}
