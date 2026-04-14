using JobListingsAPI.Data;
using JobListingsAPI.Filters;
using JobListingsAPI.Middleware;
using JobListingsAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────
//  1. SERVICE REGISTRATION
// ─────────────────────────────────────────────

// EF Core with SQLite — database file created in the project root
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=jobs.db"));

// Scoped lifetime: one instance per HTTP request
// Controller → IJobService → JobService → AppDbContext
builder.Services.AddScoped<IJobService, JobService>();

// ValidateJobFilter must be registered so ServiceFilter<T> can resolve it via DI
builder.Services.AddScoped<ValidateJobFilter>();

builder.Services.AddControllers();

// Swagger/OpenAPI for manual testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Job Listings Board API", Version = "v1" });
});

var app = builder.Build();

// ─────────────────────────────────────────────
//  2. DATABASE BOOTSTRAP
// ─────────────────────────────────────────────
// Auto-create the SQLite database & schema on first run (no migrations needed)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ─────────────────────────────────────────────
//  3. MIDDLEWARE PIPELINE (order matters!)
// ─────────────────────────────────────────────

// Built-in #1 — UseExceptionHandler
// Placed FIRST so it wraps the entire downstream pipeline.
// Any unhandled exception anywhere will be caught and returned as a clean error response.
app.UseExceptionHandler("/error");

// Custom middleware — RequestLoggerMiddleware
// Placed early (after exception handler) so every request, including failed ones,
// is logged with its method, path, status code, and duration.
app.UseRequestLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Built-in #2 — UseRouting
// Must come before UseAuthorization and MapControllers.
// Matches incoming requests to their route templates before authorization decisions are made.
app.UseRouting();

// Built-in #3 — UseAuthorization
// Placed after UseRouting so the framework knows which endpoint is matched
// before enforcing authorization policies on it.
app.UseAuthorization();

app.MapControllers();

app.Run();
