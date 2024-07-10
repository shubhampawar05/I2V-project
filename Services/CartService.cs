using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .ToListAsync();
        }

        public async Task<bool> AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductID == cartItem.ProductID);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
            }
            else
            {
                await _context.CartItems.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductID == cartItem.ProductID);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveCartItemAsync(int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductID == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
