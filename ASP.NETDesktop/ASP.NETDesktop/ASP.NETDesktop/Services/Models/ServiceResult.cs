namespace ASP.NETDesktop.Services.Models {
    public class ServiceResult {
        public static ServiceResult Ok() => new ServiceResult {IsSuccess = true};
        public static ServiceResult Fail(string error) => new ServiceResult {IsSuccess = false, Error = error};

        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }
    }

    public class ServiceResult<TOut> : ServiceResult {
        public static ServiceResult<TOut> Ok(TOut data) => new ServiceResult<TOut> {IsSuccess = true, Data = data};
        public new static ServiceResult<TOut> Fail(string error) => new ServiceResult<TOut> {IsSuccess = false, Error = error};
        public TOut Data { get; private set; }
    }
}
