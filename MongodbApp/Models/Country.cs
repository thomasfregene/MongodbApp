using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongodbApp.Models
{
    [Serializable]
    public class Country
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; set; }
        [BsonElement("country_code"), BsonRepresentation(BsonType.String)]
        public string? CountryCode { get; set; }
        [BsonElement("country_name"), BsonRepresentation(BsonType.String)]
        public string? CountryName { get; set; }
    }

    public class CountryLookedUp : Country
    {
        public List<Province> ProvinceList { get; set; } = new List<Province>();
    }
}
