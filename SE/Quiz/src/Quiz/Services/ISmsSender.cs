using System.Threading.Tasks;

namespace Quiz.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}