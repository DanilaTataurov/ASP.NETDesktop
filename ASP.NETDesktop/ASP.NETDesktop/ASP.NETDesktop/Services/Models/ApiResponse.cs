namespace ASP.NETDesktop.Services.Models {
    public class ApiResponse {
        public static ApiResponse Ok() => new ApiResponse {IsSuccess = true};
        public static ApiResponse Ok(string response) => new ApiResponse {IsSuccess = true, Message = response};
        public string Message { get; set; }
        public static ApiResponse Fail(string response) => new ApiResponse {IsSuccess = false, Error = response};

        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }
    }

    public class ApiResponse<TOut> : ApiResponse {
        public static ApiResponse<TOut> Ok(TOut data) => new ApiResponse<TOut> {IsSuccess = true, Data = data};
        public new static ApiResponse<TOut> Fail(FailedApiResponse response) => new ApiResponse<TOut> {IsSuccess = false, Error = response.Message};
        public TOut Data { get; private set; }
    }
}
