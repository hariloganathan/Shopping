namespace Shopping.Core.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public ProductName ProductName { get; set; }
        public Decimal Price { get; set; }
    }
}