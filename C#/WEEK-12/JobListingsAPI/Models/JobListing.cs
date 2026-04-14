namespace JobListingsAPI.Models
{
    public class JobListing
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;      // Ex: "Backend Developer"
        public string Company { get; set; } = string.Empty;    // Ex: "Tech Corp"
        public string Location { get; set; } = string.Empty;   // Ex: "Cairo"
        public decimal Salary { get; set; }                    // Ex: 15000
        public bool IsActive { get; set; } = true;             // soft-delete flag
        public DateTime PostedAt { get; set; }                 // auto-set on creation
    }
}
