// Question 2 - Tight Coupling Problem
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
