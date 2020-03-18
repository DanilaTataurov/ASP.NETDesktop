﻿using System;
using ASP.NETDesktop.Common.Enums;
using Prism.Mvvm;

namespace ASP.NETDesktop.Models {
    public class VacationModel : BindableBase {
        private Guid _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _comment;
        private Guid _developerId;
        public VacationStatus _status;

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public DateTime StartDate { get => _startDate; set => SetProperty(ref _startDate, value); }
        public DateTime EndDate { get => _endDate; set => SetProperty(ref _endDate, value); }
        public string Comment { get => _comment; set => SetProperty(ref _comment, value); }
        public VacationStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public Guid DeveloperId { get => _developerId; set => SetProperty(ref _developerId, value); }
    }
}
