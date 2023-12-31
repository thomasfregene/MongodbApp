using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongodbApp.Dtos
{
    public class CountryDto
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
    }
}
