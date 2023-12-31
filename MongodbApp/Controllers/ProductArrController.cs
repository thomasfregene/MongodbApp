using Microsoft.AspNetCore.Mvc;
using MongodbApp.Contracts;
using MongodbApp.Dtos;

namespace MongodbApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductArrController : ControllerBase
    {
        private readonly IProductArrService _productArrService;

        public ProductArrController(IProductArrService productArrService)
        {
            _productArrService = productArrService;
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create(ProductArrDto productArrDto)
        {
            var result = await _productArrService.Create(productArrDto);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _productArrService.GetAll();
            return Ok(result);
        }

        /*[HttpPut("update")]
        public async Task<ActionResult> Update(ProductArrDto productArrDto)
        {
            var result = await _productArrService.Update(productArrDto);
            return Ok(result);
        }*/

        [HttpGet("unwind")]
        public async Task<ActionResult> Unwind()
        {
            var result = await _productArrService.UnWind();
            return Ok(result);
        }
    }
}
