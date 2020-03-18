using System.Text.RegularExpressions;

namespace ASP.NETDesktop.Helpers {
    public class UrlHelper {
        //public static string baseUrl = "http://192.168.1.135/";
        public static string baseUrl = "http://192.168.0.109/";

        public static string Token = "Token";
        public static string Logout = "api/Auth/Logout";

        public static string ProjectList = "api/Project/List";
        public static string GetProject = "api/Project/Get";
        public static string CreateProject = "api/Project/Create";
        public static string UpdateProject = "api/Project/Update";
        public static string DeleteProject = "api/Project/Delete";

        public static string DeveloperList = "api/Developer/List";
        public static string GetDeveloper = "api/Developer/Get";
        public static string CreateDeveloper = "api/Developer/Create";
        public static string UpdateDeveloper = "api/Developer/Update";
        public static string DeleteDeveloper = "api/Developer/Delete";

        public static string VacationList = "api/Vacation/List";
        public static string DeveloperVacations = "api/Vacation/ListByDeveloperId";
        public static string GetVacation = "api/Vacation/Get";
        public static string CreateVacation = "api/Vacation/Create";
        public static string UpdateVacation = "api/Vacation/Update";
        public static string DeleteVacation = "api/Vacation/Delete";

        public static string AddDeveloperProject = "api/Developer/AddProject";
        public static string DeleteDeveloperProject = "api/Developer/DeleteProject";

        public static string UrlEncode(string str) {
            if (str != null) {
                return Regex.Replace(str, @"([^\w\-_\.~])", new MatchEvaluator(x => string.Format("{0:X}", x.Value[0])));
            } else {
                return null;
            }
        }
    }
}
