using EcomBackend.DTOs;
using EcomBackend.Models;
using EcomBackend.Repositories;

namespace EcomBackend.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICartRepository _cartRepo;

        public CartService(IProductRepository productRepo, ICartRepository cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public async Task AddToCart(AddToCartDTO cartDTO)
        {
            var product = await _productRepo.GetProductById(cartDTO.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {cartDTO.ProductId} not found.");

            var existingItem = await _cartRepo.GetByUserAndProduct(1, cartDTO.ProductId);

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
                await _cartRepo.Add(cartItem);
            }

            await _cartRepo.SaveChanges();
        }

        public async Task<CartDTO> GetCart()
        {
            var allItems = await _cartRepo.GetByUser(1);
            var cart = new CartDTO
            {
                Items = allItems.Select(c => new CartItemDTO
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    ProductName = c.Product != null ? c.Product.Name : string.Empty,
                    UnitPrice = c.Product != null ? c.Product.Price : 0,
                    Quantity = c.Quantity
                }).ToList()
            };

            return cart;
        }

        public async Task UpdateCartItem(int id, UpdateCartItemDTO cartUpdateDTO)
        {
            var item = await _cartRepo.GetById(id);
            if (item == null) throw new KeyNotFoundException("Cart item not found");

            item.Quantity = cartUpdateDTO.Quantity;
            await _cartRepo.Update(item);
        }

        public async Task DeleteCartItem(int id)
        {
            await _cartRepo.Delete(id);
            await _cartRepo.SaveChanges();
        }
    }
}