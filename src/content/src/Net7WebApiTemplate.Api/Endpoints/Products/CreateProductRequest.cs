namespace Net7WebApiTemplate.Api.Endpoints.Products
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}