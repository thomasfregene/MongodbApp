using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Contracts
{
    public interface IProvinceService
    {
        Task<ResponseModel<bool>> Insert(ProvinceDto provinceDto);
    }
}
