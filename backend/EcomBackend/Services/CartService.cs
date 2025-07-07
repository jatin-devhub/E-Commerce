using EcomBackend.Data;
using EcomBackend.DTOs;
using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Services
{
    public class CartService
    {
        private readonly AppDbContext _db;

        public CartService(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddToCart(AddToCartDTO cartDTO)
        {
            var product = await _db.Products.FindAsync(cartDTO.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {cartDTO.ProductId} not found.");
                
            var existingItem = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == 1 && c.ProductId == cartDTO.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += cartDTO.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = 1,
                    ProductId = cartDTO.ProductId,
                    Quantity = cartDTO.Quantity
                };
                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();
        }
    }
}