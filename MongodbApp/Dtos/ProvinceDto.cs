using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongodbApp.Dtos
{
    public class ProvinceDto
    {
        public string? ProvinceName { get; set; }
        public string? CountryId { get; set; }
    }
}
