namespace FiorelloAPI.DTOs.Products
{
    public class ProductMainImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string MainImage { get; set; }
    }
}
