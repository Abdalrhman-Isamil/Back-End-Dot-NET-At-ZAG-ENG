using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

namespace ShopEasy.Services;

/// <summary>
/// US-040: Apply discount | US-041: Bulk delete expired discounts
/// </summary>
public class PaymentDiscountService
{
    private readonly AppDbContext _context;
    public PaymentDiscountService(AppDbContext context) => _context = context;

    // ── US-040 ────────────────────────────────────────────────────────────
    public async Task<decimal> ApplyDiscountAsync(int orderId, string code)
    {
        var discount = await _context.Discounts
            .SingleOrDefaultAsync(d => d.Code == code);

        if (discount is null)
        {
            Console.WriteLine("[Error] Discount code not found.");
            return 0;
        }

        if (!discount.IsActive || discount.ExpiresAt < DateTime.UtcNow)
        {
            Console.WriteLine("[Error] Discount code is expired or inactive.");
            return 0;
        }

        if (discount.CurrentUses >= discount.MaxUses)
        {
            Console.WriteLine("[Error] Discount code has reached its maximum uses.");
            return 0;
        }

        var order = await _context.Orders.FindAsync(orderId);
        if (order is null) { Console.WriteLine("[Error] Order not found."); return 0; }

        decimal discountAmount = order.TotalAmount * (discount.Percentage / 100m);
        order.TotalAmount -= discountAmount;
        discount.CurrentUses++;

        await _context.SaveChangesAsync();
        Console.WriteLine($"[OK] Discount '{code}' applied. Saved {discountAmount:C}. New total: {order.TotalAmount:C}.");
        return order.TotalAmount;
    }

    // ── US-041 ────────────────────────────────────────────────────────────
    public async Task BulkDeleteExpiredDiscountsAsync()
    {
        // EF7 ExecuteDeleteAsync — no tracking, no loading
        int deleted = await _context.Discounts
            .Where(d => d.ExpiresAt < DateTime.UtcNow || !d.IsActive)
            .ExecuteDeleteAsync();

        Console.WriteLine($"[OK] {deleted} expired/inactive discount(s) deleted.");
    }

    public async Task ListDiscountsAsync()
    {
        var discounts = await _context.Discounts.AsNoTracking().ToListAsync();
        Console.WriteLine("\n🏷️  Active Discounts:");
        foreach (var d in discounts)
            Console.WriteLine($"  [{d.Code,-12}] {d.Percentage}% off  |  Expires: {d.ExpiresAt:dd MMM yyyy}  |  Uses: {d.CurrentUses}/{d.MaxUses}  |  Active: {d.IsActive}");
    }
}
