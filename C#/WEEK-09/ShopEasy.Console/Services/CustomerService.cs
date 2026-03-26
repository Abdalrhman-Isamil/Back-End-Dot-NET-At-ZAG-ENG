using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;

namespace ShopEasy.Services;

/// <summary>
/// US-010: Register customer | US-011: View profile | US-012: Update address
/// </summary>
public class CustomerService
{
    private readonly AppDbContext _context;
    public CustomerService(AppDbContext context) => _context = context;

    // ── US-010 ────────────────────────────────────────────────────────────
    public async Task RegisterCustomerAsync(string fullName, string email, string? phone,
        string address, string city, string postalCode, string nationalId)
    {
        if (await _context.Customers.AnyAsync(c => c.Email == email))
        {
            Console.WriteLine($"[Error] Email '{email}' is already registered.");
            return;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var customer = new Customer { FullName = fullName, Email = email, PhoneNumber = phone };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var profile = new CustomerProfile
            {
                CustomerId = customer.CustomerId,
                Address    = address,
                City       = city,
                PostalCode = postalCode,
                NationalId = nationalId
            };
            _context.CustomerProfiles.Add(profile);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            Console.WriteLine($"[OK] Customer '{fullName}' registered with ID {customer.CustomerId}.");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // ── US-011 ────────────────────────────────────────────────────────────
    public async Task ViewProfileAsync(int customerId)
    {
        var customer = await _context.Customers
            .Include(c => c.Profile)
            .Include(c => c.Orders)
            .SingleOrDefaultAsync(c => c.CustomerId == customerId);

        if (customer is null)
        {
            Console.WriteLine("[Error] Customer not found.");
            return;
        }

        Console.WriteLine($"\n{'─',60}");
        Console.WriteLine($"  Customer   : {customer.FullName}");
        Console.WriteLine($"  Email      : {customer.Email}");
        Console.WriteLine($"  Phone      : {customer.PhoneNumber ?? "N/A"}");
        Console.WriteLine($"  Created At : {customer.CreatedAt:dd MMM yyyy}");

        if (customer.Profile is not null)
        {
            Console.WriteLine($"  Address    : {customer.Profile.Address}, {customer.Profile.City} {customer.Profile.PostalCode}");
            Console.WriteLine($"  National ID: {customer.Profile.NationalId}");
        }
        Console.WriteLine($"  Orders     : {customer.Orders.Count} total");
        Console.WriteLine($"{'─',60}\n");
    }

    // ── US-012 ────────────────────────────────────────────────────────────
    public async Task UpdateAddressAsync(int customerId, string address, string city, string postalCode)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer is null) { Console.WriteLine("[Error] Customer not found."); return; }

        // Explicit loading
        await _context.Entry(customer).Reference(c => c.Profile).LoadAsync();

        if (customer.Profile is null)
        {
            // Upsert pattern — create if missing
            customer.Profile = new CustomerProfile
            {
                CustomerId = customerId,
                Address    = address,
                City       = city,
                PostalCode = postalCode
            };
            _context.CustomerProfiles.Add(customer.Profile);
        }
        else
        {
            customer.Profile.Address    = address;
            customer.Profile.City       = city;
            customer.Profile.PostalCode = postalCode;
            _context.CustomerProfiles.Update(customer.Profile);
        }

        await _context.SaveChangesAsync();
        Console.WriteLine("[OK] Address updated successfully.");
    }
}
