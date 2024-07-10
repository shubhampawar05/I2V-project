using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyDiscount([FromBody] DiscountRequest discountRequest)
        {
            if (string.IsNullOrWhiteSpace(discountRequest.DiscountCode) || discountRequest.CartTotal <= 0)
            {
                return BadRequest("Invalid discount request data.");
            }

            try
            {
                decimal totalAfterDiscount = await _discountService.ApplyDiscount(discountRequest.DiscountCode, discountRequest.CartTotal);
                return Ok(new { TotalAfterDiscount = totalAfterDiscount });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class DiscountRequest
    {
        public string DiscountCode { get; set; }
        public decimal CartTotal { get; set; }
    }
}
