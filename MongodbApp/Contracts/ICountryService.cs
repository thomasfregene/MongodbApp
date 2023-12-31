using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Contracts
{
    public interface ICountryService
    {
        Task<ResponseModel<bool>> Insert(CountryDto countryDto);
        Task<ResponseModel<dynamic>> GetCountryWithProvince();
    }
}
