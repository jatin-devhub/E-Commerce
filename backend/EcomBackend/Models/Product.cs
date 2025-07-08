namespace EcomBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailabilityQty { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductRelation> RelatedFrom { get; set; } = new();
        public List<ProductRelation> RelatedTo   { get; set; } = new();
    }
}