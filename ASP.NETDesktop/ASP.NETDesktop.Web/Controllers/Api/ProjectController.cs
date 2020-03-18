using System;
using System.Threading.Tasks;
using System.Web.Http;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Common.Dtos;
using ASP.NETDesktop.Domain.Interfaces.Services;
using AutoMapper;
using Newtonsoft.Json;

namespace ASP.NETDesktop.Web.Controllers.Api {
    [Authorize]
    public class ProjectController : ApiController {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

        public ProjectController(IMapper mapper, IProjectService projectService) {
            _mapper = mapper;
            _projectService = projectService;
        }

        [Route("api/Project/List")]
        [HttpGet]
        public async Task<IHttpActionResult> ListAsync() {
            try {
                var projects = _projectService.List();
                return Ok(JsonConvert.SerializeObject(projects));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Project/Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id) {
            try {
                var project = await _projectService.GetByIdAsync(id);
                return Ok(JsonConvert.SerializeObject(project));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Project/Create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromUri]ProjectApiModel model) {
            try {
                _projectService.Create(_mapper.Map<ProjectDto>(model));
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Project/Update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAsync([FromUri]ProjectApiModel model) {
            try {
                await _projectService.UpdateAsync(_mapper.Map<ProjectDto>(model));
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Project/Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteAsync(Guid id) {
            try {
                await _projectService.DeleteAsync(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
