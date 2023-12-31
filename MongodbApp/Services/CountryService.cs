using MongoDB.Driver;
using MongodbApp.Contracts;
using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;
        IMongoCollection<Country> countryCollection;
        IMongoCollection<Province> provinceCollection;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("SampleConnection");
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            countryCollection = database.GetCollection<Country>("country");
            provinceCollection = database.GetCollection<Province>("province");

        }

        public async Task<ResponseModel<bool>> Insert(CountryDto countryDto)
        {
            var countryList = new List<Country>
            {
                new Country
                {
                    CountryCode= "US",
                    CountryName ="United State of America"
                },
                new Country
                {
                    CountryCode= "UK",
                    CountryName ="United Kingdom"
                },
            };

            await countryCollection.InsertManyAsync(countryList);

            return new ResponseModel<bool>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = true
            };
        }

        public async Task<ResponseModel<dynamic>> GetCountryWithProvince()
        {
            var result = countryCollection.Aggregate()
                .Lookup<Country, Province, CountryLookedUp>(provinceCollection, a=>a.CountryId, a=>a.CountryId, a=>a.ProvinceList)
                .ToEnumerable() //to flaten the list
                .SelectMany(a => a.ProvinceList.Select(b => new
                {
                    a.CountryId,
                    a.CountryCode,
                    a.CountryName,
                    b.ProvinceId,
                    b.ProvinceName
                }))
                .ToList();

            var response = new ResponseModel<dynamic>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = result
            };

            return await Task.FromResult(response);
        }
    }
}
