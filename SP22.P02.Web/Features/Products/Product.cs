using System.Collections;

namespace SP22.P02.Web.Features
{
    public class Product : ProductDto
    {
        public int Id { get; set; }
    }
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
    }

    public class ProductGetbyIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }

    public class ProductEditDto
    {
        
        public string Name { get; set; }
        public decimal price { get; set; }
        public decimal SalePrice { get; set; }
    }
}

