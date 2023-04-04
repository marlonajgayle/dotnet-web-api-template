﻿namespace NetWebApiTemplate.Api.Endpoints.Products
{
    public class UpdateProductRequest
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}