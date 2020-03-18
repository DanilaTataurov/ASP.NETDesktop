using Prism.Mvvm;

namespace ASP.NETDesktop.Models {
    public class DeveloperModel : BindableBase {
        private string _firstName;
        private string _lastName;
        private string _grade;
        private string _location;
        private string _room;
        private string _skype;
        private string _email;
        private string _homePhone;
        private string _cellPhone;
        private string _schedule;

        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        public string Grade { get => _grade; set => SetProperty(ref _grade, value); }
        public string Location { get => _location; set => SetProperty(ref _location, value); }
        public string Room { get => _room; set => SetProperty(ref _room, value); }
        public string Skype { get => _skype; set => SetProperty(ref _skype, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string HomePhone { get => _homePhone; set => SetProperty(ref _homePhone, value); }
        public string CellPhone { get => _cellPhone; set => SetProperty(ref _cellPhone, value); }
        public string Schedule { get => _schedule; set => SetProperty(ref _schedule, value); }
    }
}
