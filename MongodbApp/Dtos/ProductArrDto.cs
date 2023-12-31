using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongodbApp.Dtos
{
    public class ProductArrDto
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public List<string> Colors { get; set; } = new List<string>();
    }

    public class ProductArrUnwindResult 
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }
        [BsonElement("product_code"), BsonRepresentation(BsonType.String)]
        public string? ProductCode { get; set; }
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public string? ProductName { get; set; }
        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        [BsonElement("colors")]
        public string? Colors { get; set; }
    }

}
