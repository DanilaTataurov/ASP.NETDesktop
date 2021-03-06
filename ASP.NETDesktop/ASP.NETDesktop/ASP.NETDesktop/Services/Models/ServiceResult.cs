﻿using Newtonsoft.Json;

namespace ASP.NETDesktop.Services.Models {
    public class ServiceResult {
        public static ServiceResult Ok() => new ServiceResult {IsSuccess = true};
        public static ServiceResult Fail(string error) => new ServiceResult {IsSuccess = false, Error = error};

        public static ServiceResult State(ApiResponse message) {
            if (message.IsSuccess) {
                return Ok();
            } else {
                return Fail(message.Error);
            }
        }

        public bool IsSuccess { get; protected set; }
        public string Error { get; protected set; }
    }

    public class ServiceResult<TOut> : ServiceResult {
        public static ServiceResult<TOut> Ok(TOut data) => new ServiceResult<TOut> {IsSuccess = true, Data = data};
        public new static ServiceResult<TOut> Fail(string error) => new ServiceResult<TOut> {IsSuccess = false, Error = error};
        public TOut Data { get; private set; }

        public static ServiceResult<TOut> State(ApiResponse response) {
            if (response.IsSuccess) {
                var jsonResult = JsonConvert.DeserializeObject(response.Message).ToString();
                TOut result = JsonConvert.DeserializeObject<TOut>(jsonResult);
                return Ok(result);
            } else {
                return Fail(response.Error);
            }
        }
    }
}
