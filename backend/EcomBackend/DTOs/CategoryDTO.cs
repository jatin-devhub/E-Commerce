namespace EcomBackend.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryDTO> SubCategories { get; set; } = [];
    }
}