using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongodbApp.Models;

namespace MongodbApp.Dtos
{
    public class ProductArrDto
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }

    public class ProductArrUnwindResult 
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }
        [BsonElement("product_code"), BsonRepresentation(BsonType.String)]
        public string? ProductCode { get; set; }
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public string? ProductName { get; set; }
        [BsonElement("product_variant_list")]
        public ProductVariant? ProductVariant { get; set; }
    }

}
