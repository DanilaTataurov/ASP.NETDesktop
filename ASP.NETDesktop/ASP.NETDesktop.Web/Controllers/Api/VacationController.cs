using System;
using System.Threading.Tasks;
using System.Web.Http;
using ASP.NETDesktop.Common.ApiModels;
using ASP.NETDesktop.Domain.Interfaces.Services;
using ASP.NETDesktop.Domain.Models.Dtos;
using AutoMapper;
using Newtonsoft.Json;

namespace ASP.NETDesktop.Web.Controllers.Api {
    [Authorize]
    public class VacationController : ApiController {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;

        public VacationController(IMapper mapper, IVacationService vacationService) {
            _mapper = mapper;
            _vacationService = vacationService;
        }

        [Route("api/Vacation/List")]
        [HttpGet]
        public async Task<IHttpActionResult> ListAsync() {
            try {
                var vacations = _vacationService.List();
                return Ok(JsonConvert.SerializeObject(vacations));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Vacation/ListByDeveloperId")]
        [HttpGet]
        public async Task<IHttpActionResult> ListByDeveloperIdAsync(Guid id) {
            try {
                var vacations = _vacationService.ListByDeveloperId(id);
                return Ok(JsonConvert.SerializeObject(vacations));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


        [Route("api/Vacation/Get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id) {
            try {
                var vacation = await _vacationService.GetByIdAsync(id);
                return Ok(JsonConvert.SerializeObject(vacation));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Vacation/Create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromUri]VacationApiModel model) {
            try {
                var result = _vacationService.Create(_mapper.Map<VacationDto>(model));
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Vacation/Update")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateAsync([FromUri]VacationApiModel model) {
            try {
                var result = _vacationService.Update(_mapper.Map<VacationDto>(model));
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/Vacation/Delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteAsync(Guid id) {
            try {
                await _vacationService.DeleteAsync(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
