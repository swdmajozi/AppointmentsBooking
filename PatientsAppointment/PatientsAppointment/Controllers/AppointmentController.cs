using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientAsppointment.Data;
using PatientsAppointment.Models;
using PatientsAppointment.Services;
using PatientsAppointment.ViewModels;


namespace PatientsAppointment.Controllers
{
    public class AppointmentController : Controller
    {
      
            private readonly ApplicationDbContext _db;
            private readonly IEmailSender _emailSender;

            // Clinic working hours and slot length
            private static readonly TimeOnly ClinicOpen = new TimeOnly(9, 0);
            private static readonly TimeOnly ClinicClose = new TimeOnly(12, 30);
            private static readonly TimeSpan SlotLength = TimeSpan.FromMinutes(15);

            public AppointmentController(ApplicationDbContext db, IEmailSender emailSender)
            {
                _db = db;
                _emailSender = emailSender;
            }

            public IActionResult Index()
            {
                var vm = new BookAppointmentViewModel
                {
                    SelectedDate = DateOnly.FromDateTime(DateTime.Today)
                };
                vm.Slots = GenerateSlots(vm.SelectedDate);
                return View(vm);
            }

            [HttpGet]
            public async Task<IActionResult> GetAvailableSlots(DateOnly date)
            {
                var slots = GenerateSlots(date);
                var booked = await _db.Appointments
                    .Where(a => a.Date == date)
                    .Select(a => new { a.StartTime, a.EndTime })
                    .ToListAsync();

                foreach (var s in slots)
                {
                    s.IsAvailable = !booked.Any(b => b.StartTime == s.Start && b.EndTime == s.End);
                }

                return PartialView("_TimeSlotsPartial", slots);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Book(string patientName, string patientEmail, DateOnly date, TimeOnly start, TimeOnly end, AppointmentType type)
            {
                if (string.IsNullOrWhiteSpace(patientName) || string.IsNullOrWhiteSpace(patientEmail) || type == AppointmentType.None)
                {
                    TempData["Error"] = "Please fill all required fields and select appointment type.";
                    return RedirectToAction("Index");
                }

            // ensure the slot is still free
            var exists = await _db.Appointments.AnyAsync(a => a.Date == date && a.StartTime == start && a.EndTime == end);
            if (exists)
            {
                TempData["Error"] = "Selected time slot is no longer available.";
                return RedirectToAction("Index");
            }

            var appointment = new Appointment
                {
                    PatientName = patientName,
                    PatientEmail = patientEmail,
                    Date = date,
                    StartTime = start,
                    EndTime = end,
                    Type = type
                };
                _db.Appointments.Add(appointment);           
                await _db.SaveChangesAsync();

          
            

                // Send confirmation (placeholder)
                await _emailSender.SendEmailAsync(patientEmail, "Appointment confirmation",
                    $"Dear {patientName},\n\nYour appointment is booked on {date:yyyy-MM-dd} from {start:HH:mm} to {end:HH:mm}.\n\nType: {type}.");

                TempData["Success"] = "Appointment booked. A confirmation email has been sent.";
                return RedirectToAction("Index");
            }

            private List<SlotViewModel> GenerateSlots(DateOnly date)
            {
                var slots = new List<SlotViewModel>();
                var t = ClinicOpen;
                while (t < ClinicClose)
                {
                    var end = t.AddMinutes(SlotLength.TotalMinutes);
                    slots.Add(new SlotViewModel { Start = t, End = end, IsAvailable = true });
                    t = end;
                }
                return slots;
            }
        }
    }

