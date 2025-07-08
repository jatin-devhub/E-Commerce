namespace EcomBackend.Models
{
    public class ProductRelation
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public int RelatedProductId { get; set; }
        public Product RelatedProduct { get; set; } = null!;
    }
}
