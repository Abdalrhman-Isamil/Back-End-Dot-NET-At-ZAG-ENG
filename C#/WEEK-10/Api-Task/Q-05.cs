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