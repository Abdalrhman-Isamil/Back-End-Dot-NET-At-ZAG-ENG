// What is Dependency?
// Dependency: أي كلاس بيعتمد على كلاس تاني عشان يشتغل.

// OrderService هنا معتمد على
// - SqlConnection (للداتابيز)
// - EmailSender (للإيميل)
// - FileLogger (لوج)

// المشكلة
// الكلاس مربوط مباشرة بالكلاسات دي (Tight Coupling)
// يعني صعب تغيرهم أو تعمل Testing بسهولة

// احسن حل
// نستخدم Dependency Injection عشان نفصل بينهم ونخلي الكود مرن أكتر 

// ========================================================================================

// Question 2 - Tight Coupling Problem
// What's the difference between A and B? Which one is better?

// in case A:
// Direct dependency (Tight Coupling)
// UserService بيعمل new لـ EmailService بنفسه
// Testing  المشكلة صعب تغييره أو عمل

// in case B:
// Dependency Injection + Interface
// UserService بياخد IEmailService من بره
// Flexible + سهل التغيير +  Testing سهل 

// الاحسن
//  B لأنه Loose Coupling وأحسن في التصميم

// ========================================================================================

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

// ========================================================================================

// Question 4 - DI Container Registration
// What is the output for each registration?

// Registration A: Transient
// New instance every time it's requested
// email1 != email2 → AreSameInstance = false

// Registration B: Scoped
// Same instance within the same request
// email1 == email2 → AreSameInstance = true

// Registration C: Singleton
// One instance for the entire application lifetime
// email1 == email2 → AreSameInstance = true

// ========================================================================================

// Question 5 - Multiple Registrations
// Which implementation will Controller A receive?
// How many services will be injected into Controller B?

// Controller A:
// When injecting a single IEmailService
// The LAST registered service is used
// → MailgunEmailService

// Controller B:
// When injecting IEnumerable<IEmailService>
// All registered services are injected
// → 3 services (SmtpEmailService, SendGridEmailService, MailgunEmailService)