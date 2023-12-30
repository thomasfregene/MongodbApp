namespace MongodbApp.Dtos
{
    public class ProductResponseDto
    {
        public string? ProductId { get; set; }

        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductResponseWithCount
    {
        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
        public string Count { get; set; }
    }
}
