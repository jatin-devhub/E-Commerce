using System.ComponentModel.DataAnnotations;

namespace EcomBackend.DTOs
{
    public class UpdateCartItemDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}