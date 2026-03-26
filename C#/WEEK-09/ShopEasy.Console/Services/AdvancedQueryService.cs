using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

namespace ShopEasy.Services;

/// <summary>
/// US-050: Lazy loading | US-051: Split queries | US-052: Customers with no orders | US-053: Products by qty sold
/// </summary>
public class AdvancedQueryService
{
    private readonly AppDbContext _context;
    public AdvancedQueryService(AppDbContext context) => _context = context;

    // ── US-050: Lazy Loading ──────────────────────────────────────────────
    public async Task DemoLazyLoadingAsync(int productId)
    {
        Console.WriteLine("\n🔄 Lazy Loading Demo:");
        Console.WriteLine("  Loading product WITHOUT explicit Include...");

        // With UseLazyLoadingProxies(), accessing Reviews fires a DB query on demand
        var product = await _context.Products
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(p => p.ProductId == productId);

        if (product is null) { Console.WriteLine("[Error] Product not found."); return; }

        Console.WriteLine($"  Product loaded: {product.Name}");
        Console.WriteLine("  Accessing Reviews navigation (lazy query fires now)...");

        // This access triggers lazy load — observe the extra SQL in the console
        int reviewCount = product.Reviews.Count;
        Console.WriteLine($"  ✅ Reviews loaded lazily: {reviewCount} review(s).");
    }

    // ── US-051: Split Queries ─────────────────────────────────────────────
    public async Task DemoSplitQueryAsync(int customerId)
    {
        Console.WriteLine("\n✂️  Split Query Demo (AsSplitQuery):");

        var customer = await _context.Customers
            .AsSplitQuery()
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
            .Include(c => c.Reviews)
            .SingleOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer is null) { Console.WriteLine("[Error] Customer not found."); return; }

        Console.WriteLine($"  Customer: {customer.FullName}");
        Console.WriteLine($"  Orders  : {customer.Orders.Count}");
        Console.WriteLine($"  Reviews : {customer.Reviews.Count}");
        Console.WriteLine("  ✅ Data loaded using multiple SELECT statements (no cartesian explosion).");
    }

    // ── US-052: Customers with NO orders (Left Join / GroupJoin) ─────────
    public async Task FindCustomersWithNoOrdersAsync()
    {
        Console.WriteLine("\n📭 Customers With No Orders:");

        // Method 1: using !Any() — most readable
        var noOrders = await _context.Customers
            .AsNoTracking()
            .Where(c => !c.Orders.Any())
            .Select(c => new { c.FullName, c.Email })
            .ToListAsync();

        if (!noOrders.Any())
        {
            Console.WriteLine("  All customers have placed at least one order.");
            return;
        }

        foreach (var c in noOrders)
            Console.WriteLine($"  📧 {c.FullName,-25}  {c.Email}");

        // Method 2: GroupJoin (left join demonstration)
        Console.WriteLine("\n  (GroupJoin approach for left-join demo)");
        var allCustomers = await _context.Customers.AsNoTracking().ToListAsync();
        var allOrders    = await _context.Orders.AsNoTracking().ToListAsync();

        var result = allCustomers
            .GroupJoin(allOrders,
                c => c.CustomerId,
                o => o.CustomerId,
                (c, orders) => new { c.FullName, OrderCount = orders.Count() })
            .Where(x => x.OrderCount == 0)
            .ToList();

        foreach (var r in result)
            Console.WriteLine($"  GroupJoin hit: {r.FullName}");
    }

    // ── US-053: Products ranked by total quantity sold (Inner Join) ───────
    public async Task ProductsByQtySoldAsync()
    {
        Console.WriteLine("\n📊 Products Ranked by Total Quantity Sold:");

        // Inner Join between Products and OrderItems using Join()
        var products = await _context.Products.IgnoreQueryFilters().AsNoTracking().ToListAsync();
        var orderItems = await _context.OrderItems.AsNoTracking().ToListAsync();

        var ranked = products
            .Join(orderItems,
                p  => p.ProductId,
                oi => oi.ProductId,
                (p, oi) => new { p.ProductId, p.Name, oi.Quantity })
            .GroupBy(x => new { x.ProductId, x.Name })
            .Select(g => new { ProductName = g.Key.Name, TotalSold = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.TotalSold)
            .ToList();

        int rank = 1;
        foreach (var r in ranked)
            Console.WriteLine($"  {rank++,2}. {r.ProductName,-35}  Sold: {r.TotalSold}");

        if (!ranked.Any())
            Console.WriteLine("  No sales data found.");
    }

    // Extra: Distinct tag names
    public async Task ShowDistinctTagNamesAsync()
    {
        Console.WriteLine("\n🏷️  Distinct Tags on Active Products:");
        var tags = await _context.ProductTags
            .Include(pt => pt.Tag)
            .Select(pt => pt.Tag.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToListAsync();

        foreach (var t in tags) Console.WriteLine($"  · {t}");
    }
}
