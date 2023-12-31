using MongoDB.Driver;
using MongodbApp.Contracts;
using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Services
{

    public class ProvinceService : IProvinceService
    {
        private readonly IConfiguration _configuration;
        IMongoCollection<Province> provinceCollection;

        public ProvinceService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("SampleConnection");
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            provinceCollection = database.GetCollection<Province>("province");
        }

        public async Task<ResponseModel<bool>> Insert(ProvinceDto provinceDto)
        {
            var provinceList = new List<Province>
            {
                new Province
                {
                    ProvinceName="Califonia",
                    CountryId="65915ec09803317270ddb1b1",
                },
                new Province
                {
                    ProvinceName="Texas",
                    CountryId="65915ec09803317270ddb1b1",
                },
                new Province
                {
                    ProvinceName="Florida",
                    CountryId="65915ec09803317270ddb1b1",
                },
                new Province
                {
                    ProvinceName="Wales",
                    CountryId="65915ec09803317270ddb1b2",
                },
                new Province
                {
                    ProvinceName="Scotland",
                    CountryId="65915ec09803317270ddb1b2",
                },
                new Province
                {
                    ProvinceName="Ireland",
                    CountryId="65915ec09803317270ddb1b2",
                }
            };

            await provinceCollection.InsertManyAsync(provinceList);

            return new ResponseModel<bool>
            {
                Code = "00",
                Message = "Operation Successful",
                Data = true
            };
        }
    }
}
