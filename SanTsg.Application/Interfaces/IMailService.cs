using SanTsgProject.Application.Models;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Interfaces
{

    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
    
}
