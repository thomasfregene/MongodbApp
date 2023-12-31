using Microsoft.AspNetCore.Mvc;
using MongodbApp.Contracts;
using MongodbApp.Dtos;

namespace MongodbApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(ProvinceDto provinceDto)
        {
            var result = await _provinceService.Insert(provinceDto);
            return Ok(result);
        }
    }
}
