using System.Threading.Tasks;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services.Interfaces {
    public interface IAccountService {
        Task<ServiceResult> LoginAsync(string userName, string password);
        Task<ServiceResult> LogoutAsync();
    }
}
