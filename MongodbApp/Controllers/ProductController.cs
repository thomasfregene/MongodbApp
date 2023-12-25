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

        [HttpDelete("/{productCode}/delete")]
        public async Task<ActionResult> Delete(string productCode)
        {
            var result = await _productService.Delete(productCode);

            return Ok(result);
        }
    }
}
