using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NETDesktop.Common.ApiModels.Account;
using ASP.NETDesktop.Helpers;
using ASP.NETDesktop.Services.Interfaces;
using ASP.NETDesktop.Services.Models;
using Newtonsoft.Json;

namespace ASP.NETDesktop.Services {
    public class ApiService : IApiService {
        public async Task<ApiResponse> DoRequestAsync(string method, string url, object parameters) {
            try {
                IDictionary<string, string> dictionaryParameters = ConvertHelper.ParametersToDictionary(parameters);
                string data = string.Join("&", dictionaryParameters
                    .Select(pair => string.Concat(UrlHelper.UrlEncode(pair.Key), "=", UrlHelper.UrlEncode(pair.Value))));

                var request = (HttpWebRequest) HttpWebRequest.Create(url + "?" + data);
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = 0;
                request.KeepAlive = true;
                request.Method = method;
                request.Headers.Set("X-API-KEY", "343b1ae9-fb57-45fd-90f0-g1e097f9d621");

                string token = ApplicationPropertiesHelper.GetToken();

                request.Headers.Set("Authorization", "Bearer " + token);
                var response = (HttpWebResponse) (request.GetResponseAsync().Result);

                using (var stream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        string result = await reader.ReadToEndAsync();
                        return ApiResponse.Ok(result);
                    }
                }
            } catch (Exception ex) {
                return ApiResponse.Fail(ex.Message);
            }
        }

        public async Task<ApiResponse> DoLoginAsync(string username, string password) {
            try {
                HttpClient client = new HttpClient();
                var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "self"), 
                    new KeyValuePair<string, string>("username", username), 
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage response = await client.PostAsync(UrlHelper.baseUrl + UrlHelper.Token, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonConvert.DeserializeObject<TokenResult>(responseString);

                if (tokenResult.AccessToken != null) {
                    return ApiResponse.Ok(tokenResult.AccessToken);
                } else {
                    return ApiResponse.Fail(response.ToString());
                }
            } catch (Exception ex) {
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}
