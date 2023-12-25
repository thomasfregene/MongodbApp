using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Contracts
{
    public interface IProductService
    {
        Task<ResponseModel<bool>> Insert(ProductDto product);
        Task<ResponseModel<bool>> Update(ProductDto product);
        Task<ResponseModel<bool>> Delete(string productCode);
        Task<ResponseModel<List<ProductResponseDto>>> GetAll();
    }
}
