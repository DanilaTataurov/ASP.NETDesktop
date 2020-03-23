using System.Text.RegularExpressions;

namespace ASP.NETDesktop.Helpers {
    public class UrlHelper {
        //public static string baseUrl = "http://192.168.1.135/";
        public static string baseUrl = "http://192.168.0.109/";

        public static string Token = baseUrl + "Token";
        public static string Logout = baseUrl + "api/Auth/Logout";

        public static string ProjectList = baseUrl + "api/Project/List";
        public static string GetProject = baseUrl + "api/Project/Get";
        public static string CreateProject = baseUrl + "api/Project/Create";
        public static string UpdateProject = baseUrl + "api/Project/Update";
        public static string DeleteProject = baseUrl + "api/Project/Delete";

        public static string DeveloperList = baseUrl + "api/Developer/List";
        public static string GetDeveloper = baseUrl + "api/Developer/Get";
        public static string CreateDeveloper = baseUrl + "api/Developer/Create";
        public static string UpdateDeveloper = baseUrl + "api/Developer/Update";
        public static string DeleteDeveloper = baseUrl + "api/Developer/Delete";

        public static string VacationList = baseUrl + "api/Vacation/List";
        public static string DeveloperVacations = baseUrl + "api/Vacation/ListByDeveloperId";
        public static string GetVacation = baseUrl + "api/Vacation/Get";
        public static string CreateVacation = baseUrl + "api/Vacation/Create";
        public static string UpdateVacation = baseUrl + "api/Vacation/Update";
        public static string DeleteVacation = baseUrl + "api/Vacation/Delete";

        public static string AddDeveloperProject = baseUrl + "api/Developer/AddProject";
        public static string DeleteDeveloperProject = baseUrl + "api/Developer/DeleteProject";

        public static string UrlEncode(string str) {
            if (str != null) {
                return Regex.Replace(str, @"([^\w\-_\.~])", new MatchEvaluator(x => string.Format("{0:X}", x.Value[0])));
            } else {
                return null;
            }
        }
    }
}
