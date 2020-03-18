using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETDesktop.Helpers {
    public static class ConvertHelper {
        public static IDictionary<string, string> ParametersToDictionary(object postParameters) {
            Type t = postParameters.GetType();
            IDictionary<string, string> dictionaryParams = t.GetProperties().ToDictionary(
                x => x.Name,
                x => x.GetValue(postParameters, null)?.ToString()
            );
            return dictionaryParams;
        }
    }
}
