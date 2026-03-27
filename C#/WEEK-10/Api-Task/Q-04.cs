// Question 4 - DI Container Registration
// What is the output for each registration ?

// Registration A : Transient
// New instance every time it's requested
// email1 != email2 → AreSameInstance = false

// Registration B : Scoped
// Same instance within the same request
// email1 == email2 → AreSameInstance = true

// Registration C : Singleton
// One instance for the entire application lifetime
// email1 == email2 → AreSameInstance = true
