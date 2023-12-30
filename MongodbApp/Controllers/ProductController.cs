using Microsoft.AspNetCore.Mvc;
using MongodbApp.Contracts;
using MongodbApp.Dtos;

namespace MongodbApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var result =  await _productService.GetAll();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(ProductDto productDto)
        {
            var result = await _productService.Insert(productDto);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(ProductDto productDto)
        {
            var result = await _productService.Update(productDto);

            return Ok(result);
        }

        [HttpDelete("{productCode}/delete")]
        public async Task<ActionResult> Delete(string productCode)
        {
            var result = await _productService.Delete(productCode);

            return Ok(result);
        }

        [HttpPost("bulkInsert")]
        public async Task<ActionResult> BulkInsert(IFormFile file)
        {
            var result = await _productService.BulkInsert(file);

            return Ok(result);
        }

        [HttpPut("bulkUpdate")]
        public async Task<ActionResult> BulkUpdate()
        {
            var result = await _productService.BulkUpdate();

            return Ok(result);
        }

        [HttpDelete("bulkDelete")]
        public async Task<ActionResult> BulkDelete()
        {
            var result = await _productService.BulkDelete();
            return Ok(result);
        }

        [HttpPost("upsert")]
        public async Task<ActionResult> Upsert(ProductDto productDto)
        {
            var result = await _productService.Upsert(productDto);
            return Ok(result);
        }

        [HttpPost("bulkwrite")]
        public async Task<ActionResult> BulkWrite()
        {
            var result = await _productService.BulkWrite();
            return Ok(result);
        }

        [HttpGet("{id}/getbyid")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpPost("createindex")]
        public async Task<ActionResult> CreateIndex()
        {
            var result = await _productService.CreateIndex();

            return Ok(result);
        }
    }
}
