using System.Threading.Tasks;

namespace Quiz.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}