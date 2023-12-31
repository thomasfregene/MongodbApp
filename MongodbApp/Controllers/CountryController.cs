using Microsoft.AspNetCore.Mvc;
using MongodbApp.Contracts;
using MongodbApp.Dtos;

namespace MongodbApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CountryDto countryDto)
        {
            var result = await _countryService.Insert(countryDto);
            return Ok(result);
        }

        [HttpGet("getcountrywithprovince")]
        public async Task<ActionResult> GetAll()
        {
            var result =  await _countryService.GetCountryWithProvince();
            return Ok(result);
        }
    }
}
