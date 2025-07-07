namespace EcomBackend.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; } = new();
        public decimal CartTotal => Items.Sum(i => i.TotalPrice);
    }
}