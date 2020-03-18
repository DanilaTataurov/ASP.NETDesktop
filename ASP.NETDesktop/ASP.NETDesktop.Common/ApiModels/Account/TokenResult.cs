using Newtonsoft.Json;

namespace ASP.NETDesktop.Common.ApiModels.Account {
    public class TokenResult {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
