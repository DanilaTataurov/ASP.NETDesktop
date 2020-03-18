using System;
using Prism.Mvvm;

namespace ASP.NETDesktop.Models {
    public class ProjectModel : BindableBase {
        private string _name;
        private string _description;
        private string _client;
        private string _company;
        private string _source;
        private string _contact;
        private string _status;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _gitUrl;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public string Client { get => _client; set => SetProperty(ref _client, value); }
        public string Company { get => _company; set => SetProperty(ref _company, value); }
        public string Source { get => _source; set => SetProperty(ref _source, value); }
        public string Contact { get => _contact; set => SetProperty(ref _contact, value); }
        public string Status { get => _status; set => SetProperty(ref _status, value); }
        public DateTime StartDate { get => _startDate; set => SetProperty(ref _startDate, value); }
        public DateTime EndDate { get => _endDate; set => SetProperty(ref _endDate, value); }
        public string GitUrl { get => _gitUrl; set => SetProperty(ref _gitUrl, value); }
    }
}
