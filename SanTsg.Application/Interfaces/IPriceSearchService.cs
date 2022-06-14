using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{
    public interface IPriceSearchService
    {
        Task<List<Hotels>> GetHotels(string cityId, string token);
    }
}
