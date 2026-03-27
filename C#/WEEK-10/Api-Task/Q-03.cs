// Question 3 - Constructor Injection
// What happens when a request is received?

// When a request is received :
// 1.The Controller instance is created
// 2.ASP.NET Core detects that ProductController needs ProductService
// 3.It creates ProductService
// 4.ProductService requires IRepository
// 5.The DI Container injects SqlRepository (registered as AddScoped)
// 6.Everything is wired together automatically

// Result :
// ProductService uses SqlRepository without creating it manually
//  Loose Coupling + cleaner code + easier maintenance and testing
