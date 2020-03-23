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
    public class ProjectService : BaseService<Project>, IProjectService {
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<ProjectDto> List() {
            List<Project> projects = _unitOfWork.GetRepository<Project>().All()
                .Include(d => d.Developers)
                .OrderBy(x => x.Name)
                .ToList();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetByIdAsync(Guid id) {
            Project entity = await _unitOfWork.GetRepository<Project>().FindByIdAsync(id);
            return _mapper.Map<ProjectDto>(entity);
        }

        public void Create(ProjectDto dto) {
            Project entity = new Project();
            entity = _mapper.Map<Project>(dto);

            _unitOfWork.GetRepository<Project>().Add(entity);
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(ProjectDto dto) {
            Project entity = GetById(dto.Id);
            entity.Client = dto.Client;
            entity.Company = dto.Company;
            entity.Contact = dto.Contact;
            entity.Description = dto.Description;
            entity.Name = dto.Name;
            entity.Status = dto.Status;
            entity.Source = dto.Source;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.GitUrl = dto.GitUrl;

            _unitOfWork.GetRepository<Project>().Update(entity);
            _unitOfWork.Commit();
        }

        public async Task<bool> DeleteAsync(Guid id) {
            try {
                Project entity = GetById(id);
                if (entity == null) {
                    return false;
                }

                _unitOfWork.GetRepository<Project>().Remove(id);
                _unitOfWork.Commit();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
