namespace Net7WebApiTemplate.Domain.Entities
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public ProductCatergory ProductCatergory { get; set; } = new ProductCatergory();

    }
}