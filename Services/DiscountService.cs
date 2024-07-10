using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IDiscountService
    {
        Task<decimal> ApplyDiscount(string discountCode, decimal cartTotal);
    }

    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> ApplyDiscount(string discountCode, decimal cartTotal)
        {
            var discount = await _context.Discounts
                                         .FirstOrDefaultAsync(d => d.DiscountCode == discountCode);

            if (discount == null)
            {
                throw new ArgumentException("Invalid discount code.");
            }

            decimal discountAmount = cartTotal * (discount.DiscountPercentage / 100);
            decimal totalAfterDiscount = cartTotal - discountAmount;

            return totalAfterDiscount;
        }
    }
}
