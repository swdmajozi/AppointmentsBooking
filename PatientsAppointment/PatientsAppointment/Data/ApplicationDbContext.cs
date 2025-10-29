using Microsoft.EntityFrameworkCore;
using PatientAppointment.Models;

namespace PatientAppointment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; } = null!;
    }
}