namespace NetWebApiTemplate.Domain.Entities
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public ProductCategory ProductCategory { get; set; } = new ProductCategory();

    }
}