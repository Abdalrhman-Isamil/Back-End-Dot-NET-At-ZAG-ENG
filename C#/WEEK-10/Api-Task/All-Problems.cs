// ===================== Question 1 =====================
// What is Dependency?
// Dependency: when a class relies on another class to work.

// Example:
// OrderService depends on:
// - SqlConnection (for database)
// - EmailSender (for sending emails)
// - FileLogger (for logging)

// Problem:
// The class is directly tied to these implementations (Tight Coupling)
// → Hard to change or test

// Better approach:
// Use Dependency Injection to decouple them and make the code more flexible


// ===================== Question 2 =====================
// Tight Coupling Problem
// What's the difference between A and B? Which one is better?

// Scenario A:
// Direct dependency (Tight Coupling)
// UserService creates EmailService using 'new'
// Problem: hard to change or test

// Scenario B:
// Dependency Injection + Interface
// UserService receives IEmailService from outside
// Advantage: flexible + easy to change + easy to test

// Which is better?
// Scenario B because it uses Loose Coupling and better design


// ===================== Question 3 =====================
// Constructor Injection
// What happens when a request is received?

// When a request is received:
// 1. The Controller instance is created
// 2. ASP.NET Core detects that ProductController needs ProductService
// 3. It creates ProductService
// 4. ProductService requires IRepository
// 5. The DI Container injects SqlRepository (registered as AddScoped)
// 6. Everything is wired together automatically

// Result:
// ProductService uses SqlRepository without creating it manually
// → Loose Coupling + cleaner code + easier maintenance and testing


// ===================== Question 4 =====================
// DI Container Registration
// What is the output for each registration?

// Transient:
// New instance every time it's requested
// → AreSameInstance = false

// Scoped:
// Same instance within the same request
// → AreSameInstance = true

// Singleton:
// One instance for the entire application lifetime
// → AreSameInstance = true


// ===================== Question 5 =====================
// Multiple Registrations
// Which implementation will Controller A receive?
// How many services will be injected into Controller B?

// Controller A:
// The LAST registered service is used
// → MailgunEmailService

// Controller B:
// All registered services are injected as IEnumerable
// → 3 services (SmtpEmailService, SendGridEmailService, MailgunEmailService)
