using System.Threading.Tasks;
using ASP.NETDesktop.Services.Models;

namespace ASP.NETDesktop.Services.Interfaces {
    public interface IApiService {
        Task<ApiResponse> DoRequestAsync(string method, string uri, object postParameters);
        Task<ApiResponse> DoLoginAsync(string username, string password);
    }
}
