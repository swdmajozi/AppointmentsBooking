namespace PatientsAppointment.Services
{
    public class ConsoleEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine("=== Sending Email ===");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("=====================");
            return Task.CompletedTask;
        }
    }
}
