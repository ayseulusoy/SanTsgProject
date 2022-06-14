using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{
    public interface IGetArrivalAutocompleteService
    {
        Task<string> GetCityId(GetArrivalAutocompleteRequest.Root cityIdRequest, string cityName, string token);
    }
}
