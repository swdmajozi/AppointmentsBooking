
using System.ComponentModel.DataAnnotations;

namespace PatientsAppointment.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string PatientEmail { get; set; } = null!;

        [Required]
        public string PatientName { get; set; } = null!;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public AppointmentType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}