
using PatientsAppointment.Models;

namespace PatientsAppointment.ViewModels
{
    public class SlotViewModel
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public bool IsAvailable { get; set; }
        public string Label => $"{Start:HH:mm} - {End:HH:mm}";
    }

    public class BookAppointmentViewModel
    {
        public DateOnly SelectedDate { get; set; }
        public AppointmentType SelectedType { get; set; }
        public List<SlotViewModel> Slots { get; set; } = new();
    }
}