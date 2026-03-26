using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

namespace ShopEasy.Services;

/// <summary>
/// US-020: Browse | US-021: Search | US-022: Details | US-023: Top-5 | US-024: Bulk deactivate
/// </summary>
public class ProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context) => _context = context;

    // ── US-020 ────────────────────────────────────────────────────────────
    public async Task BrowseActiveProductsAsync()
    {
        // Global query filter (IsActive=true) applies automatically
        var products = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .OrderBy(p => p.Price)
            .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name })
            .ToListAsync();

        Console.WriteLine($"\n{'─',65}");
        Console.WriteLine($"  {"Product",-35} {"Price",10}  Category");
        Console.WriteLine($"{'─',65}");
        foreach (var p in products)
            Console.WriteLine($"  {p.Name,-35} {p.Price,10:C}  {p.CategoryName}");
        Console.WriteLine($"{'─',65}\n");
    }

    // ── US-021 ────────────────────────────────────────────────────────────
    public async Task SearchProductsAsync(string keyword)
    {
        var results = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.Name.Contains(keyword) || p.Category.Name.Contains(keyword))
            .ToListAsync();

        if (!results.Any())
        {
            Console.WriteLine($"[Info] No products found matching '{keyword}'.");
            return;
        }

        Console.WriteLine($"\nSearch results for '{keyword}':");
        foreach (var p in results)
            Console.WriteLine($"  [{p.ProductId}] {p.Name} — {p.Price:C} ({p.Category.Name})");
    }

    // ── US-022 ────────────────────────────────────────────────────────────
    public async Task ViewProductDetailsAsync(int productId)
    {
        var product = await _context.Products
            .IgnoreQueryFilters()
            .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.Reviews)
            .SingleOrDefaultAsync(p => p.ProductId == productId);

        if (product is null) { Console.WriteLine("[Error] Product not found."); return; }

        var avgRating = product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;
        var reviewCount = product.Reviews.Count;

        Console.WriteLine($"\n{'─',60}");
        Console.WriteLine($"  {product.Name} ({product.SKU})");
        Console.WriteLine($"  Price   : {product.Price:C}  |  Stock: {product.StockQuantity}");
        Console.WriteLine($"  Active  : {product.IsActive}");
        Console.WriteLine($"  Tags    : {string.Join(", ", product.ProductTags.Select(pt => pt.Tag.Name))}");
        Console.WriteLine($"  Reviews : {reviewCount} reviews, avg rating: {avgRating:F1}/5");
        foreach (var r in product.Reviews)
            Console.WriteLine($"    ⭐ {r.Rating}/5 — {r.Comment}");
        Console.WriteLine($"{'─',60}\n");
    }

    // ── US-023 ────────────────────────────────────────────────────────────
    public async Task GetTop5HighestRatedAsync()
    {
        var top5 = await _context.Reviews
            .AsNoTracking()
            .GroupBy(r => new { r.ProductId, r.Product.Name })
            .Select(g => new
            {
                ProductName   = g.Key.Name,
                AverageRating = g.Average(r => r.Rating),
                TotalReviews  = g.Count()
            })
            .OrderByDescending(x => x.AverageRating)
            .Take(5)
            .ToListAsync();

        Console.WriteLine("\n🏆 Top 5 Highest-Rated Products:");
        int rank = 1;
        foreach (var p in top5)
            Console.WriteLine($"  {rank++}. {p.ProductName,-35} Avg: {p.AverageRating:F2} ({p.TotalReviews} reviews)");
    }

    // ── US-024 ────────────────────────────────────────────────────────────
    public async Task DeactivateOutOfStockAsync()
    {
        // EF7 ExecuteUpdateAsync — bulk, no tracking
        int count = await _context.Products
            .IgnoreQueryFilters()
            .Where(p => p.StockQuantity == 0)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsActive, false));

        Console.WriteLine($"[OK] {count} out-of-stock product(s) deactivated.");

        // Verify
        int inactive = await _context.Products.IgnoreQueryFilters()
            .CountAsync(p => !p.IsActive);
        Console.WriteLine($"[Info] Total inactive products now: {inactive}");
    }

    // Extra: MaxBy / MinBy — most expensive product in each category
    public async Task MostExpensivePerCategoryAsync()
    {
        var results = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .GroupBy(p => p.Category.Name)
            .Select(g => new
            {
                Category = g.Key,
                MaxPrice = g.Max(p => p.Price),
                MinPrice = g.Min(p => p.Price)
            })
            .ToListAsync();

        Console.WriteLine("\n📊 Price Range Per Category:");
        foreach (var r in results)
            Console.WriteLine($"  {r.Category,-20} Min: {r.MinPrice:C}  Max: {r.MaxPrice:C}");
    }
}
