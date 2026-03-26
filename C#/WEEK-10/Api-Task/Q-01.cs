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
