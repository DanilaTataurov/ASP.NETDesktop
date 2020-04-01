using Newtonsoft.Json;

namespace ASP.NETDesktop.Models.Responses {
    public class FailedResult {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
