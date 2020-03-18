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
    public class DeveloperController : ApiController {
        private readonly IMapper _mapper;
        private readonly IDeveloperService _developerService;

        public DeveloperController(IMapper mapper, IDeveloperService developerService) {
            _mapper = mapper;
            _developerService = developerService;
        }

        [Route("api/Developer/List")]
        [HttpGet]
        public async Task<IHttpActionResult> ListAsync() {
            try {
                var developers = _developerService.List();
                return Ok(JsonConvert.SerializeObject(developers));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Developer/Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id) {
            try {
                var developer = await _developerService.GetByIdAsync(id);
                return Ok(JsonConvert.SerializeObject(developer));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Developer/Create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromUri]DeveloperApiModel model) {
            try {
                _developerService.Create(_mapper.Map<DeveloperDto>(model));
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Developer/Update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAsync([FromUri]DeveloperApiModel model) {
            try {
                await _developerService.UpdateAsync(_mapper.Map<DeveloperDto>(model));
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Developer/Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteAsync(Guid id) {
            try {
                await _developerService.DeleteAsync(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //DeveloperProjects
        [Route("api/Developer/AddProject")]
        [HttpPost]
        public async Task<IHttpActionResult> AddProjectAsync(Guid developerId, Guid projectId) {
            try {
                await _developerService.AddProjectAsync(developerId, projectId);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Developer/DeleteProject")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleleProjectAsync(Guid developerId, Guid projectId) {
            try {
                await _developerService.DeleteProjectAsync(developerId, projectId);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
