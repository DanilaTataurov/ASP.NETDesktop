namespace ASP.NETDesktop.Domain.Interfaces.Services.Responses {
    public class ServiceResult {
        public static ServiceResult Ok(string message) => new ServiceResult { IsSuccess = true, Message = message };
        public static ServiceResult Fail(string message) => new ServiceResult { IsSuccess = false, Message = message };

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
