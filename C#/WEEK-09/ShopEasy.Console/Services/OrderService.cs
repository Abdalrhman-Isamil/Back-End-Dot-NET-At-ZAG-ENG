using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

namespace ShopEasy.Services;

/// <summary>
/// US-030: Place order | US-031: Order history | US-032: Cancel | US-033: Revenue report | US-034: Raw SQL
/// </summary>
public class OrderService
{
    private readonly AppDbContext _context;
    public OrderService(AppDbContext context) => _context = context;

    // ── US-030 ────────────────────────────────────────────────────────────
    public async Task PlaceOrderAsync(int customerId, Dictionary<int, int> productQuantities,
        PaymentMethod paymentMethod)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Load products (ignore global filter to allow seeing all)
            var productIds = productQuantities.Keys.ToList();
            var products = await _context.Products
                .IgnoreQueryFilters()
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            // Build order items and compute total
            var orderItems = new List<OrderItem>();
            decimal total = 0;
            foreach (var p in products)
            {
                int qty = productQuantities[p.ProductId];
                if (p.StockQuantity < qty)
                    throw new InvalidOperationException($"Not enough stock for '{p.Name}'.");

                p.StockQuantity -= qty;
                total += p.Price * qty;
                orderItems.Add(new OrderItem { ProductId = p.ProductId, Quantity = qty, UnitPrice = p.Price });
            }

            var order = new Order
            {
                CustomerId  = customerId,
                TotalAmount = total,
                Status      = OrderStatus.Pending,
                OrderItems  = orderItems
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Create payment
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Amount  = total,
                Method  = paymentMethod,
                Status  = PaymentStatus.Pending
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            Console.WriteLine($"[OK] Order #{order.OrderId} placed. Total: {total:C}. Stock updated.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"[Error] {ex.Message}");
        }
    }

    // ── US-031 ────────────────────────────────────────────────────────────
    public async Task ViewOrderHistoryAsync(int customerId)
    {
        var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)
            .Include(o => o.Payment)
            .OrderByDescending(o => o.PlacedAt)
            .ToListAsync();

        var latest = orders.FirstOrDefault();
        Console.WriteLine($"\n📦 Orders for Customer #{customerId}  ({orders.Count} total)");

        foreach (var o in orders)
        {
            Console.WriteLine($"\n  Order #{o.OrderId}  Status: {o.Status}  Total: {o.TotalAmount:C}  Placed: {o.PlacedAt:dd MMM yyyy}");
            foreach (var oi in o.OrderItems)
                Console.WriteLine($"    ↳ ProductId {oi.ProductId}  x{oi.Quantity}  @ {oi.UnitPrice:C}");
            if (o.Payment is not null)
                Console.WriteLine($"    💳 Payment: {o.Payment.Method} — {o.Payment.Status}");
        }

        if (latest is not null)
            Console.WriteLine($"\n  Most recent order: #{latest.OrderId} on {latest.PlacedAt:dd MMM yyyy}");
    }

    // ── US-032 ────────────────────────────────────────────────────────────
    public async Task CancelOrderAsync(int orderId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .SingleOrDefaultAsync(o => o.OrderId == orderId);

            if (order is null) { Console.WriteLine("[Error] Order not found."); return; }
            if (order.Status != OrderStatus.Pending)
            {
                Console.WriteLine($"[Error] Cannot cancel order with status '{order.Status}'.");
                return;
            }

            // Restore stock
            var productIds = order.OrderItems.Select(oi => oi.ProductId).ToList();
            var products = await _context.Products
                .IgnoreQueryFilters()
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            foreach (var oi in order.OrderItems)
            {
                var product = products.Single(p => p.ProductId == oi.ProductId);
                product.StockQuantity += oi.Quantity;
            }

            order.Status = OrderStatus.Cancelled;
            if (order.Payment is not null)
                order.Payment.Status = PaymentStatus.Refunded;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            Console.WriteLine($"[OK] Order #{orderId} cancelled and stock restored.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"[Error] {ex.Message}");
        }
    }

    // ── US-033 ────────────────────────────────────────────────────────────
    public async Task MonthlyRevenueReportAsync()
    {
        var report = await _context.Orders
            .Where(o => o.Status == OrderStatus.Delivered && o.PlacedAt.Year == DateTime.UtcNow.Year)
            .GroupBy(o => o.PlacedAt.Month)
            .Select(g => new { Month = g.Key, Revenue = g.Sum(o => o.TotalAmount) })
            .OrderBy(g => g.Month)
            .ToListAsync();

        Console.WriteLine($"\n📈 Monthly Revenue ({DateTime.UtcNow.Year}):");
        if (!report.Any()) { Console.WriteLine("  No delivered orders this year."); return; }
        foreach (var r in report)
            Console.WriteLine($"  {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(r.Month),-12}: {r.Revenue:C}");
    }

    // ── US-034 ────────────────────────────────────────────────────────────
    public async Task ViewPendingOrdersRawSqlAsync()
    {
        Console.WriteLine("\n🔍 Pending Orders (via Raw SQL / Stored Procedure):");

        // Part 1 — FromSqlInterpolated
        var status = "Pending";
        var orders = await _context.Orders
            .FromSqlInterpolated($"SELECT * FROM shop.Orders WHERE Status = {status}")
            .AsNoTracking()
            .ToListAsync();

        // Part 2 — calling stored procedure GetPendingOrders (created via migration)
        // var spOrders = await _context.Orders.FromSqlRaw("EXEC shop.GetPendingOrders").ToListAsync();

        foreach (var o in orders)
            Console.WriteLine($"  Order #{o.OrderId}  Customer#{o.CustomerId}  Total: {o.TotalAmount:C}  Placed: {o.PlacedAt:dd MMM yyyy}");

        if (!orders.Any()) Console.WriteLine("  No pending orders.");
    }
}
