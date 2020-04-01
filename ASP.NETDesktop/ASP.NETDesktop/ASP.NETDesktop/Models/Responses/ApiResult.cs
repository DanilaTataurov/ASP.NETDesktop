namespace ASP.NETDesktop.Models.Responses {
    public class ApiResult {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ApiResult<TOut> {
        public bool IsSuccess { get; set; }
        public TOut Message { get; set; }
    }
}
