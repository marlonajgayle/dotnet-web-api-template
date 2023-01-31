namespace Net7WebApiTemplate.Application.Features.Products.Queries.GetAllProducts
{
    public class ProductsDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}