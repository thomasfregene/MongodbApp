using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongodbApp.Models
{
    [Serializable]
    public class Province
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ProvinceId { get; set; }
        [BsonElement("province_name")]
        public string? ProvinceName { get; set; }
        [BsonElement("country_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; set; }
    }
}
