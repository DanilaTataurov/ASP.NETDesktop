using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETDesktop.BLL.Services.Base;
using ASP.NETDesktop.Domain.Entities;
using ASP.NETDesktop.Domain.Interfaces;
using ASP.NETDesktop.Domain.Interfaces.Services;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;

namespace ASP.NETDesktop.BLL.Services {
    public class DeveloperService : BaseService<Developer>, IDeveloperService {
        public DeveloperService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<DeveloperDto> List() {
            List<Developer> developers = _unitOfWork.GetRepository<Developer>().All()
                .Include(v => v.Vacations)
                .Include(p => p.Projects)
                .OrderBy(x => x.FirstName)
                .ThenBy(x=>x.LastName)
                .ToList(); ;
            return _mapper.Map<IEnumerable<DeveloperDto>>(developers);
        }

        public async Task<DeveloperDto> GetByIdAsync(Guid id) {
            Developer entity = await _unitOfWork.GetRepository<Developer>().FindByIdAsync(id);
            return _mapper.Map<DeveloperDto>(entity);
        }

        public void Create(DeveloperDto dto) {
            Developer entity = new Developer();
            entity = _mapper.Map<Developer>(dto);

            _unitOfWork.GetRepository<Developer>().Add(entity);
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(DeveloperDto dto) {
            Developer entity = GetById(dto.Id);
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
                Developer entity = GetById(id);
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
            Developer developer = GetById(developerId);
            Project project = await _unitOfWork.GetRepository<Project>().FindByIdAsync(projectId);

            var projects = developer.Projects;
            projects.Add(project);
            _unitOfWork.GetRepository<Developer>().Update(developer);
            _unitOfWork.Commit();
        }

        public async Task DeleteProjectAsync(Guid developerId, Guid projectId) {
            Developer developer = GetById(developerId);
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
