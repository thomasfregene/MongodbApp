using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Contracts
{
    public interface IProductService
    {
        Task<ResponseModel<bool>> Insert(ProductDto product);
        //Task<ResponseModel<bool>> Update(ProductDto product);
        Task<ResponseModel<ProductDto>> Update(ProductDto product);
        Task<ResponseModel<bool>> Delete(string productCode);
        Task<ResponseModel<ProductResponseWithCount>> GetAll();
        Task<ResponseModel<bool>> BulkInsert(IFormFile file);
        Task<ResponseModel<bool>> BulkUpdate();
        Task<ResponseModel<bool>> BulkDelete();
        Task<ResponseModel<bool>> Upsert(ProductDto productDto);
        Task<ResponseModel<bool>> BulkWrite();
        Task<ResponseModel<ProductResponseDto>> GetById(string id);
        Task<ResponseModel<bool>> CreateIndex();
        Task<ResponseModel<List<ProductResponseDto>>> LoadCursorData();
    }
}
