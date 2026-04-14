using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) that carries feedback data from the client.
    /// UserName and Comment come from the JSON Body.
    /// Rate comes from the Query String (demonstrated via [FromQuery] in the controller).
    /// </summary>
    public class FeedbackRequest
    {
        // ── Comes from the JSON Body ──────────────────────────────────────────
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } = string.Empty;

        // NOTE: Rate comes from the Query String via [FromQuery] in the controller.
        //       No [Range] here — the controller validates after merging the query value.
        public int Rate { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
