using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.BLL.Services.Base;
using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Common.Helpers;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces;
using ASP.NETDesktop.Domain.Interfaces.Services;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Services {
    public class VacationService : BaseService<Vacation>, IVacationService {
        public VacationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<Vacation> List() {
            return _unitOfWork.GetRepository<Vacation>().All()
                .Include(d=>d.Developer)
                .OrderBy(x=>x.StartDate)
                .ThenBy(x=>x.EndDate)
                .ToList();
        }

        public IEnumerable<Vacation> ListByDeveloperId(Guid id) {
            return _unitOfWork.GetRepository<Vacation>().All()
                .Include(d => d.Developer)
                .OrderBy(x => x.StartDate)
                .ThenBy(x=>x.EndDate)
                .Where(x => x.DeveloperId == id)
                .ToList();
        }

        public async Task<Vacation> GetByIdAsync(Guid id) {
            return await _unitOfWork.GetRepository<Vacation>().FindByIdAsync(id);
        }

        public string Create(VacationDto dto) {
            string message = CheckDates(dto);

            if (string.IsNullOrWhiteSpace(message)) {
                Vacation entity = new Vacation() {
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Comment = dto.Comment,
                    Status = dto.Status,
                    DeveloperId = dto.DeveloperId
                };
                _unitOfWork.GetRepository<Vacation>().Add(entity);
                _unitOfWork.Commit();
                return message = "Vacation successfully added.";
            }
            return message;
        }

        public async Task<string> UpdateAsync(VacationDto dto) {
            string message = CheckDates(dto);

            if (string.IsNullOrWhiteSpace(message)) { 
                Vacation entity = await GetByIdAsync(dto.Id);

                entity.StartDate = dto.StartDate;
                entity.EndDate = dto.EndDate;
                entity.Comment = dto.Comment;
                entity.Status = dto.Status;

                _unitOfWork.GetRepository<Vacation>().Update(entity);
                _unitOfWork.Commit();
                return message = "Vacation successfully updated.";
            }
            return message;
        }

        public async Task<bool> DeleteAsync(Guid id) {
            try {
                Vacation entity = await GetByIdAsync(id);
                if (entity == null) {
                    return false;
                }

                _unitOfWork.GetRepository<Vacation>().Remove(id);
                _unitOfWork.Commit();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }


        //Date comparison
        public bool DeveloperVacationMatches(VacationDto dto, Guid vacationId) {
            IEnumerable<Vacation> vacations = new List<Vacation>();

            if (vacationId != Guid.Empty) {
                vacations = List().Where(x => x.Id != vacationId);
            } else {
                vacations = List();
            }

            IEnumerable<Vacation> developerVacations = ListByDeveloperId(dto.DeveloperId);
            foreach (Vacation vacation in vacations) {
                if (DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, vacation.StartDate, vacation.EndDate)) { 
                    if (dto.DeveloperId == vacation.DeveloperId) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ProjectsVacationMatches(VacationDto dto, Guid vacationId) {
            var currentDeveloper = _unitOfWork.GetRepository<Developer>().Where(x => x.Id == dto.DeveloperId).FirstOrDefault();

            IEnumerable<Vacation> vacations = new List<Vacation>();
            if (vacationId != Guid.Empty) {
                vacations = List().Where(x => x.Id != vacationId && x.DeveloperId != dto.DeveloperId);
            }
            else {
                vacations = List().Where(v => v.DeveloperId != dto.DeveloperId);
            }

            foreach (var vacation in vacations) {
                if (DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, vacation.StartDate, vacation.EndDate)) {
                    var developer = _unitOfWork.GetRepository<Developer>().Where(x => x.Id == vacation.DeveloperId).Include(p=>p.Projects).FirstOrDefault();

                    if (currentDeveloper.Projects.Any(l => developer.Projects.Select(s => s.Id).Contains(l.Id))) {
                        return true;
                    };
                }
            }
            return false;
        }

        public bool MatchesWithTwoVacations(VacationDto dto, Guid vacationId) {
            int matchCount = 0;

            IEnumerable<Vacation> vacations = new List<Vacation>();
            if (vacationId != Guid.Empty) {
                vacations = List().Where(x => x.Id != vacationId);
            }
            else {
                vacations = List();
            }
            
            IEnumerable<Vacation> developerVacations = ListByDeveloperId(dto.DeveloperId);
            foreach (Vacation vacation in vacations) {
                if (DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, vacation.StartDate, vacation.EndDate)) {
                    matchCount++;
                }
            }

            switch (matchCount) {
                case 0: return false;
                case 1: return false;
                case 2: return true;
            }

            return false;
        }

        public string CheckDates(VacationDto dto) {
            string message = String.Empty;
            int newDays = DateHelper.GetWorkingDays(dto.StartDate, dto.EndDate);

            if (newDays == 0)
                return message = "You have chosen weekend(s) or holiday(s)";

            if (newDays > 20) {
                return message = "You cannot choose more than 20 days of vacation. You have chosen " + newDays + " day(s)";
            }

            if (DeveloperVacationMatches(dto, dto.Id)) {
                return message = "This developer already has a vacation on the appointed dates.";
            }

            if (ProjectsVacationMatches(dto, dto.Id)) {
                return message = "On the project this developer is working on, another developer has already taken vacation for these dates.";
            }

            if (MatchesWithTwoVacations(dto, dto.Id)) {
                return message = "The appointed dates are already taken by two developers.";
            }

            else {
                int oldDays = 0;
                IEnumerable<Vacation> developerVacations = ListByDeveloperId(dto.DeveloperId).Where(x => x.Id != dto.Id);
 
                foreach (var vacation in developerVacations) {
                    oldDays += DateHelper.GetWorkingDays(vacation.StartDate, vacation.EndDate);
                }

                int allDays = newDays + oldDays;

                if (allDays > 20) {
                    int freeDays = 20 - oldDays;
                    return message = "You cannot choose more than 20 days of vacation. Last selected vacation contains " + newDays + " day(s), you can choose " + freeDays + " day(s).";
                }
            }
            return message;
        }
    }
}
