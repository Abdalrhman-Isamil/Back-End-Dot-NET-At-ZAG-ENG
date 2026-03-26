using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ShopEasy.Models;

namespace ShopEasy.Data;

/// <summary>
/// US-003 — Seeds initial data from JSON files in dependency order.
/// Idempotent: skips if data already exists.
/// </summary>
public class DataSeeder
{
    private readonly AppDbContext _context;
    private static readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public DataSeeder(AppDbContext context) => _context = context;

    public async Task SeedAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await SeedCategoriesAsync();
            await SeedCustomersAsync();
            await SeedProductsAsync();
            await SeedTagsAsync();
            await SeedProductTagsAsync();
            await SeedOrdersAsync();
            await SeedOrderItemsAsync();
            await SeedReviewsAsync();
            await SeedDiscountsAsync();

            await transaction.CommitAsync();
            Console.WriteLine("[Seeder] All data seeded successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"[Seeder] Error: {ex.Message}");
            throw;
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private async Task SeedCategoriesAsync()
    {
        if (await _context.Categories.AnyAsync()) return;

        // Seed parents first, then children (two passes)
        var all = LoadJson<CategorySeedDto>("categories.json");
        var parents = all.Where(c => c.ParentCategoryId == null).ToList();
        var children = all.Where(c => c.ParentCategoryId != null).ToList();

        var parentEntities = parents.Select(c => new Category
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Slug = c.Slug,
            Description = c.Description,
            ParentCategoryId = null
        }).ToList();
        _context.Categories.AddRange(parentEntities);
        await _context.SaveChangesAsync();

        var childEntities = children.Select(c => new Category
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Slug = c.Slug,
            Description = c.Description,
            ParentCategoryId = c.ParentCategoryId
        }).ToList();
        _context.Categories.AddRange(childEntities);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Categories seeded.");
    }

    private async Task SeedCustomersAsync()
    {
        if (await _context.Customers.AnyAsync()) return;
        var data = LoadJson<Customer>("customers.json");
        _context.Customers.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Customers seeded.");
    }

    private async Task SeedProductsAsync()
    {
        if (await _context.Products.IgnoreQueryFilters().AnyAsync()) return;
        var data = LoadJson<Product>("products.json");
        _context.Products.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Products seeded.");
    }

    private async Task SeedTagsAsync()
    {
        if (await _context.Tags.AnyAsync()) return;
        var data = LoadJson<Tag>("tags.json");
        _context.Tags.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Tags seeded.");
    }

    private async Task SeedProductTagsAsync()
    {
        if (await _context.ProductTags.AnyAsync()) return;
        // Assign some tags to products manually
        var productTags = new List<ProductTag>
        {
            new() { ProductId = 1, TagId = 1 },  // iPhone -> New Arrival
            new() { ProductId = 1, TagId = 4 },  // iPhone -> Premium
            new() { ProductId = 2, TagId = 2 },  // Galaxy -> Best Seller
            new() { ProductId = 3, TagId = 4 },  // MacBook -> Premium
            new() { ProductId = 4, TagId = 2 },  // Dell -> Best Seller
            new() { ProductId = 5, TagId = 3 },  // Oxford -> Sale
            new() { ProductId = 6, TagId = 1 },  // Dress -> New Arrival
            new() { ProductId = 6, TagId = 5 },  // Dress -> Eco Friendly
        };
        _context.ProductTags.AddRange(productTags);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] ProductTags seeded.");
    }

    private async Task SeedOrdersAsync()
    {
        if (await _context.Orders.AnyAsync()) return;
        var data = LoadJson<OrderSeedDto>("orders.json");
        var orders = data.Select(o => new Order
        {
            OrderId   = o.OrderId,
            CustomerId = o.CustomerId,
            Status    = Enum.Parse<OrderStatus>(o.Status),
            TotalAmount = o.TotalAmount,
            PlacedAt  = o.PlacedAt,
            ShippedAt = o.ShippedAt
        }).ToList();
        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Orders seeded.");
    }

    private async Task SeedOrderItemsAsync()
    {
        if (await _context.OrderItems.AnyAsync()) return;
        var data = LoadJson<OrderItem>("orderItems.json");
        _context.OrderItems.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] OrderItems seeded.");
    }

    private async Task SeedReviewsAsync()
    {
        if (await _context.Reviews.AnyAsync()) return;
        var data = LoadJson<Review>("reviews.json");
        _context.Reviews.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Reviews seeded.");
    }

    private async Task SeedDiscountsAsync()
    {
        if (await _context.Discounts.AnyAsync()) return;
        var data = LoadJson<Discount>("discounts.json");
        _context.Discounts.AddRange(data);
        await _context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Discounts seeded.");
    }

    // ── JSON loader ───────────────────────────────────────────────────────
    private static List<T> LoadJson<T>(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "JsonData", fileName);
        if (!File.Exists(path))
            throw new FileNotFoundException($"Seed file not found: {path}");
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, _opts)
               ?? throw new InvalidDataException($"Could not deserialize {fileName}");
    }

    // ── Local DTOs (to handle string → enum conversion) ──────────────────
    private class CategorySeedDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public string Slug { get; set; } = "";
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    private class OrderSeedDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; }
    }
}
