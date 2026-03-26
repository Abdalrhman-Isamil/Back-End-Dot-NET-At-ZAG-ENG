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