using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Contracts
{
    public interface IProductArrService
    {
        Task<ResponseModel<bool>> Create(ProductArrDto productArrDto);
        Task<ResponseModel<dynamic>> GetAll();
        Task<ResponseModel<bool>> Update(ProductArrDto productArrDto);
        Task<ResponseModel<dynamic>> UnWind();
    }
}
