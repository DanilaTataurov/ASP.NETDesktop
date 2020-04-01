using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.BLL.Common.Models;
using ASP.NETDesktop.BLL.Services.Base;
using ASP.NETDesktop.Common.Enums;
using ASP.NETDesktop.Common.Extensions;
using ASP.NETDesktop.Common.Helpers;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces;
using ASP.NETDesktop.Domain.Interfaces.Services;
using ASP.NETDesktop.Domain.Interfaces.Services.Responses;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Services {
    public class VacationService : BaseService<Vacation>, IVacationService {
        public VacationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<VacationDto> List() {
            List<Vacation> vacations = _unitOfWork.GetRepository<Vacation>().All()
                .Include(d=>d.Developer)
                .OrderBy(x=>x.StartDate)
                .ThenBy(x=>x.EndDate)
                .ToList();
            return _mapper.Map<IEnumerable<VacationDto>>(vacations);
        }

        public IEnumerable<VacationDto> ListByDeveloperId(Guid id) {
            List<Vacation> vacations = _unitOfWork.GetRepository<Vacation>().All()
                .Include(d => d.Developer)
                .OrderBy(x => x.StartDate)
                .ThenBy(x=>x.EndDate)
                .Where(x => x.DeveloperId == id)
                .ToList();
            return _mapper.Map<IEnumerable<VacationDto>>(vacations);
        }

        public async Task<VacationDto> GetByIdAsync(Guid id) {
            Vacation entity = await _unitOfWork.GetRepository<Vacation>().FindByIdAsync(id);
            return _mapper.Map<VacationDto>(entity);
        }

        public ServiceResult Create(VacationDto dto) {
            string message = CheckDates(dto);

            if (string.IsNullOrWhiteSpace(message)) {
                Vacation entity = new Vacation();
                entity = _mapper.Map<Vacation>(dto);

                _unitOfWork.GetRepository<Vacation>().Add(entity);
                _unitOfWork.Commit();
                message = "Vacation successfully added.";
                return ServiceResult.Ok(message);
            }
            return ServiceResult.Fail(message);
        }

        public ServiceResult Update(VacationDto dto) {
            string message = CheckDates(dto);

            if (string.IsNullOrWhiteSpace(message)) { 
                Vacation entity = GetById(dto.Id);
                entity.StartDate = dto.StartDate;
                entity.EndDate = dto.EndDate;
                entity.Comment = dto.Comment;
                entity.Status = EnumExtensions.ParseDescriptionToEnum<VacationStatus>(dto.Status);

                _unitOfWork.GetRepository<Vacation>().Update(entity);
                _unitOfWork.Commit();
                message = "Vacation successfully updated.";
                return ServiceResult.Ok(message);
            }
            return ServiceResult.Fail(message);
        }

        public async Task<bool> DeleteAsync(Guid id) {
            try {
                Vacation entity = GetById(id);
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
            bool isMatch = List().Where(x => x.Id != vacationId && dto.DeveloperId == x.DeveloperId).Any(v
                => DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, v.StartDate, v.EndDate));

            if (isMatch) {
                return true;
            }
            return false;
        }

        public СomparisonResult ProjectsVacationMatches(VacationDto dto, Guid vacationId) {
            var currentDeveloper = _unitOfWork.GetRepository<Developer>().Where(x => x.Id == dto.DeveloperId).FirstOrDefault();

            IEnumerable<VacationDto> vacations = List().Where(v => v.DeveloperId != dto.DeveloperId);
            if (vacationId != Guid.Empty) {
                vacations = vacations.Where(x => x.Id != vacationId);
            }

            foreach (var vacation in vacations) {
                if (DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, vacation.StartDate, vacation.EndDate)) {
                    var developer = _unitOfWork.GetRepository<Developer>().Where(x => x.Id == vacation.DeveloperId).Include(p=>p.Projects).FirstOrDefault();

                    if (currentDeveloper.Projects.Any(l => developer.Projects.Select(s => s.Id).Contains(l.Id))) {
                        СomparisonResult result = new СomparisonResult {
                            developerId = developer.Id,
                            vacationId = vacation.Id
                        };
                        return result;
                    }
                }
            }
            return null;
        }

        public IEnumerable<VacationDto> MatchesWithTwoVacations(VacationDto dto, Guid vacationId) {
            IEnumerable<VacationDto> vacations = List().Where(x => x.Id != vacationId);
            ICollection<VacationDto> matchingVacations = new List<VacationDto>();
            
            foreach (VacationDto vacation in vacations) {
                if (DateHelper.DatesIntersect(dto.StartDate, dto.EndDate, vacation.StartDate, vacation.EndDate)) {
                    matchingVacations.Add(vacation);
                }
            }

            if (matchingVacations.Count == 2) {
                return matchingVacations;
            }
            return null;
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

            var comparisonResult = ProjectsVacationMatches(dto, dto.Id);
            if (comparisonResult != null) {
                var developer = _unitOfWork.GetRepository<Developer>().FindById(comparisonResult.developerId);
                var vacation = _unitOfWork.GetRepository<Vacation>().FindById(comparisonResult.vacationId);

                return message = "On the project this developer is working on, another developer has already taken vacation for these dates. \n\n" +
                    "Developer: " + developer.FirstName + " " + developer.LastName + "\n" + 
                    vacation.StartDate.ToString("dd/MM/yyyy") + " - " + vacation.EndDate.ToString("dd/MM/yyyy") + "\n" + 
                    "Status: " + EnumExtensions.GetDescription(vacation.Status);
            }

            IEnumerable<VacationDto> matchingVacations = MatchesWithTwoVacations(dto, dto.Id);
            if (matchingVacations != null) {
                message = "The appointed dates are already taken by two developers:\n\n";

                foreach (var vacation in matchingVacations) {
                    var developer = _unitOfWork.GetRepository<Developer>().FindById(vacation.DeveloperId);
                    message += developer.FirstName + " " + developer.LastName + "\n"
                               + vacation.StartDate.ToString("dd/MM/yyyy") + " - " + vacation.EndDate.ToString("dd/MM/yyyy") + "\n" +
                               "Status: " + vacation.Status + ".\n\n";
                }
                return message;
            }

            else {
                int oldDays = 0;
                IEnumerable<VacationDto> developerVacations = ListByDeveloperId(dto.DeveloperId).Where(x => x.Id != dto.Id);
 
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
