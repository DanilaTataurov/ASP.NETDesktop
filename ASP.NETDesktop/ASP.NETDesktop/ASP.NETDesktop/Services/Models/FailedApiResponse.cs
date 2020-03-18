namespace ASP.NETDesktop.Services.Models {
    public class FailedApiResponse {
        public string Error { private get; set; }
        public string _message;

        public string Message {
            get => _message ?? Error;
            set => _message = value;
        }
    }
}
