using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.BLL.Services.Base;
using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces;
using ASP.NETDesktop.Domain.Interfaces.Services;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Services {
    public class DeveloperService : BaseService<Developer>, IDeveloperService {
        public DeveloperService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<Developer> List() {
            return _unitOfWork.GetRepository<Developer>().All()
                .Include(v => v.Vacations)
                .Include(p => p.Projects)
                .OrderBy(x => x.FirstName)
                .ThenBy(x=>x.LastName)
                .ToList(); ;
        }

        public async Task<Developer> GetByIdAsync(Guid id) {
            return await _unitOfWork.GetRepository<Developer>().FindByIdAsync(id);
        }

        public void Create(DeveloperDto dto) {
            //TODO: add mapper
            Developer entity = new Developer() {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Grade = dto.Grade,
                Location = dto.Location,
                Room = dto.Room,
                Skype = dto.Skype,
                Email = dto.Email,
                HomePhone = dto.HomePhone,
                CellPhone = dto.CellPhone,
                Schedule = dto.Schedule
            };
            _unitOfWork.GetRepository<Developer>().Add(entity);
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(DeveloperDto dto) {
            //TODO: add mapper
            Developer entity = await GetByIdAsync(dto.Id);
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Grade = dto.Grade;
            entity.Location = dto.Location;
            entity.Room = dto.Room;
            entity.Skype = dto.Skype;
            entity.Email = dto.Email;
            entity.HomePhone = dto.HomePhone;
            entity.CellPhone = dto.CellPhone;
            entity.Schedule = dto.Schedule;

            _unitOfWork.GetRepository<Developer>().Update(entity);
            _unitOfWork.Commit();
        }

        public async Task<bool> DeleteAsync(Guid id) {
            try {
                Developer entity = await GetByIdAsync(id);
                if (entity == null) {
                    return false;
                }

                _unitOfWork.GetRepository<Developer>().Remove(id);
                _unitOfWork.Commit();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        //DeveloperProjects
        public async Task AddProjectAsync(Guid developerId, Guid projectId) {
            Developer developer = await GetByIdAsync(developerId);
            Project project = await _unitOfWork.GetRepository<Project>().FindByIdAsync(projectId);

            var projects = developer.Projects;
            projects.Add(project);
            _unitOfWork.GetRepository<Developer>().Update(developer);
            _unitOfWork.Commit();
        }

        public async Task DeleteProjectAsync(Guid developerId, Guid projectId) {
            Developer developer = await GetByIdAsync(developerId);
            Project project = await _unitOfWork.GetRepository<Project>().FindByIdAsync(projectId);

            var projects = developer.Projects;

            foreach (var item in projects) {
                if (project.Id == projectId) {
                    projects.Remove(item);
                    _unitOfWork.GetRepository<Developer>().Update(developer);
                    _unitOfWork.Commit();
                    return;
                }
            }
        }
    }
}
