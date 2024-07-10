using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/cartItem")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartItemController(CartService cartService)
        {
            _cartService = cartService;
        }

        // Get all cart items
        // Endpoint: http://localhost:5182/api/cartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            return Ok(cartItems);
        }

        // Add a new cart item
        // Endpoint: http://localhost:5182/api/cartItem
        [HttpPost]
        public async Task<ActionResult> AddCartItem(CartItem cartItem)
        {
            var result = await _cartService.AddOrUpdateCartItemAsync(cartItem);
            if (result)
            {
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false });
        }

        // Update an existing cart item
        // Endpoint: http://localhost:5182/api/cartItem/{productId}
        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateCartItem(int productId, CartItem cartItem)
        {
            if (productId != cartItem.ProductID)
            {
                return BadRequest(new { success = false, message = "Product ID mismatch" });
            }

            var result = await _cartService.UpdateCartItemAsync(cartItem);
            if (result)
            {
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false });
        }

        // Remove a cart item
        // Endpoint: http://localhost:5182/api/cartItem/{productId}
        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveCartItem(int productId)
        {
            var result = await _cartService.RemoveCartItemAsync(productId);
            if (result)
            {
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false });
        }
    }
}
