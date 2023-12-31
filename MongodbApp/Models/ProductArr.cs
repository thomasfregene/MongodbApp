using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongodbApp.Models
{
    [Serializable]
    public class ProductArr
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }
        [BsonElement("product_code"), BsonRepresentation(BsonType.String)]
        public string? ProductCode { get; set; }
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public string? ProductName { get; set; }
        [BsonElement("product_variant_list")]
        public List<ProductVariant> ProductVariantList { get; set; }  = new List<ProductVariant>();
    }

    public class ProductVariant
    {
        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        [BsonElement("color"), BsonRepresentation(BsonType.String)]
        public string? Color { get; set; }
    }
}
