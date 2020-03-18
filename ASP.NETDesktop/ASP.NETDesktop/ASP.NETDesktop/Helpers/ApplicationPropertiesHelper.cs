using System.Linq;
using Xamarin.Forms;

namespace ASP.NETDesktop.Helpers {
    public static class ApplicationPropertiesHelper {
        public static string GetToken() {
            string token = "";
            if (Application.Current.Properties.ContainsKey("token")) {
                token = Application.Current.Properties.FirstOrDefault(x => x.Key == "token").Value.ToString();
            }
            return token;
        }
    }
}
