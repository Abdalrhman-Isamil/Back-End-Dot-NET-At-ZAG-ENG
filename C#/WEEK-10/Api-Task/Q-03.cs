// Question 3 - Constructor Injection
// What happens when a request is received?

// 1. The Controller instance is created
// 2. ASP.NET Core sees that ProductController needs ProductService
// 3. It creates ProductService
// 4. ProductService needs IRepository
// 5. The DI container injects SqlRepository (registered as Scoped)
// 6. Everything is wired automatically

// Result
// ProductService uses SqlRepository without creating it manually
//  Loose Coupling + cleaner code + easier testing