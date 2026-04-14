using JobListingsAPI.Data;
using JobListingsAPI.Models;

namespace JobListingsAPI.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }

        // Returns only listings that have not been soft-deleted
        public IEnumerable<JobListing> GetAllActive()
        {
            return _context.JobListings
                           .Where(j => j.IsActive)
                           .ToList();
        }

        // Returns any listing by ID regardless of IsActive (soft-delete check)
        public JobListing? GetById(int id)
        {
            return _context.JobListings.FirstOrDefault(j => j.Id == id);
        }

        // Auto-sets PostedAt and IsActive before saving
        public void Create(JobListing job)
        {
            job.PostedAt = DateTime.Now;
            job.IsActive = true;
            _context.JobListings.Add(job);
            _context.SaveChanges();
        }

        // Updates only the mutable fields of an existing listing
        public void Update(int id, JobListing job)
        {
            var existing = _context.JobListings.FirstOrDefault(j => j.Id == id);
            if (existing == null) return;

            existing.Title    = job.Title;
            existing.Company  = job.Company;
            existing.Location = job.Location;
            existing.Salary   = job.Salary;
            _context.SaveChanges();
        }

        // Soft delete — sets IsActive = false, record stays in DB
        public void SoftDelete(int id)
        {
            var existing = _context.JobListings.FirstOrDefault(j => j.Id == id);
            if (existing == null) return;

            existing.IsActive = false;
            _context.SaveChanges();
        }
    }
}
