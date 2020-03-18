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
    public class ProjectService : BaseService<Project>, IProjectService {
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<Project> List() {
            return _unitOfWork.GetRepository<Project>().All()
                .Include(d => d.Developers)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public async Task<Project> GetByIdAsync(Guid id) {
            return await _unitOfWork.GetRepository<Project>().FindByIdAsync(id);
        }

        public void Create(ProjectDto dto) {
            //TODO: add mapper
            Project entity = new Project() {
                Client = dto.Client,
                Company = dto.Company,
                Contact = dto.Contact,
                Description = dto.Description,
                Name = dto.Name,
                Source = dto.Source,
                Status = dto.Status,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                GitUrl = dto.GitUrl
            };
            _unitOfWork.GetRepository<Project>().Add(entity);
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(ProjectDto dto) {
            //TODO: add mapper
            Project entity = await GetByIdAsync(dto.Id);
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
                Project entity = await GetByIdAsync(id);
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
