namespace PatientAppointment.Models
    {
        public enum AppointmentType
        {
            [System.ComponentModel.Description("Please Select")]
            None = 0,
            InjuryOnDuty = 1,
            Emergency = 2,
            ChronicManagement = 3,
            FirstConsultation = 4,
            FollowUp = 5
        }
    }
