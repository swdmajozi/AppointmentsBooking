using Microsoft.EntityFrameworkCore;
using PatientsAppointment.Models;

namespace PatientAsppointment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; } = null!;
    }
}