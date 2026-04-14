using JobListingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobListingsAPI.Filters
{
    /// <summary>
    /// Action filter applied to POST and PUT endpoints.
    /// Runs BEFORE the action method — blocks invalid job listings early,
    /// so bad data never reaches the service or database layer.
    /// </summary>
    public class ValidateJobFilter : IActionFilter
    {
        // Called before the action method executes
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Try to locate a JobListing argument in the action parameters
            var job = context.ActionArguments.Values
                             .OfType<JobListing>()
                             .FirstOrDefault();

            if (job == null)
            {
                context.Result = new BadRequestObjectResult("Request body is missing or invalid.");
                return;
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(job.Title))
                errors.Add("Title is required.");

            if (string.IsNullOrWhiteSpace(job.Company))
                errors.Add("Company is required.");

            if (job.Salary <= 0)
                errors.Add("Salary must be positive.");

            if (errors.Any())
            {
                // Short-circuit the pipeline — the action method never runs
                context.Result = new BadRequestObjectResult(
                    "Title and Company are required. Salary must be positive.");
            }
        }

        // Called after the action method executes (nothing to do here)
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
