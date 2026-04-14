using FeedbackAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        // ── Dependencies ──────────────────────────────────────────────────────
        private readonly IConfiguration _configuration;
        private readonly ILogger<FeedbackController> _logger;

        // ── Constructor Injection ─────────────────────────────────────────────
        /// <summary>
        /// ASP.NET Core DI container injects IConfiguration and ILogger automatically.
        /// </summary>
        public FeedbackController(IConfiguration configuration,
                                  ILogger<FeedbackController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // ── POST api/feedback ─────────────────────────────────────────────────
        /// <summary>
        /// Accepts user feedback.
        ///
        /// Model-Binding demo:
        ///   • [FromQuery] Rate   → comes from the URL query string  ?rate=4
        ///   • [FromBody]  body   → UserName + Comment come from the JSON body
        ///
        /// Example request:
        ///   POST /api/feedback?rate=4
        ///   Content-Type: application/json
        ///   { "userName": "Ahmed", "comment": "Great service!" }
        /// </summary>
        [HttpPost]
        public IActionResult SubmitFeedback(
            [FromQuery] int rate,
            [FromBody]  FeedbackRequest body)
        {
            // ── 1. Read SystemSettings from IConfiguration ────────────────────
            string systemName = _configuration["SystemSettings:SystemName"]
                                ?? "Unknown System";
            bool allowAnonymous = bool.TryParse(
                _configuration["SystemSettings:AllowAnonymousFeedback"],
                out bool anon) && anon;

            // ── 2. Guard: block anonymous if not allowed ──────────────────────
            if (!allowAnonymous &&
                string.IsNullOrWhiteSpace(body.UserName))
            {
                return BadRequest(new
                {
                    Message = "Anonymous feedback is not allowed on this system."
                });
            }

            // ── 3. Merge rate from query string into the DTO ──────────────────
            //    (demonstrates that [FromBody] and [FromQuery] work together)
            body.Rate = rate;

            // Manual validation for Rate (1–5) since [Range] was removed from DTO
            if (body.Rate < 1 || body.Rate > 5)
            {
                return BadRequest(new { Message = "Rate must be between 1 and 5." });
            }

            // ── 4. Logging ────────────────────────────────────────────────────
            // Log Information – every request
            _logger.LogInformation(
                "Feedback received from user: '{UserName}' | Rate: {Rate} | System: {SystemName}",
                body.UserName, body.Rate, systemName);

            // Log Warning – unhappy user
            if (body.Rate < 3)
            {
                _logger.LogWarning(
                    "User '{UserName}' is NOT satisfied with the service! Rate: {Rate}",
                    body.UserName, body.Rate);
            }

            // ── 5. Return success response ────────────────────────────────────
            return Ok(new
            {
                Message  = $"Thank you, {body.UserName}! Feedback received for {systemName}.",
                System   = systemName,
                UserName = body.UserName,
                Rate     = body.Rate,
                Comment  = body.Comment,
                AllowAnonymousFeedback = allowAnonymous
            });
        }
    }
}
