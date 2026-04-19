using AgdtTestTask.Core.Mapping.Extensions;
using AgdtTestTask.Core.Web.Controllers;
using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.MediatR.Commands;
using AgdtTestTask.Medical.MediatR.Queries;
using AgdtTestTask.Medical.WebApi.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgdtTestTask.Medical.WebApi.Controllers
{
    public class PatientController(
        ISender sender,
        IMapper mapper)
        : ApiControllerBase
    {
        [HttpGet("by-guid")]
        public async Task<ActionResult<PatientDisplayVM>> GetByGuid(
            [FromQuery][Required] Guid guid)
        {
            var patient = await sender.Send(
                new GetPatientByNameGuidQuery(guid));

            return OkOrNotFound(
                mapper.Map<PatientDisplayVM>(patient));
        }

        [HttpGet("by-birthdate")]
        public async Task<ActionResult<IEnumerable<PatientDisplayVM>>> GetByBirthdate(
            [FromQuery][Required] string[] birthdateParams)
        {
            var patients = await sender.Send(
                new GetPatientsByBirthdateQuery(birthdateParams));

            return Ok(mapper.MapCollection<PatientDisplayVM>(patients));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(
            [FromQuery][Required] long id)
        {
            await sender.Send(new DeletePatientCommand(id));

            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> Create(
            [FromBody] PatientCreatingVM patient)
        {
            await sender.Send(new CreatePatientCommand(
                mapper.Map<PatientDTO>(patient)));

            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> Update(
            [FromQuery][Required] long patientId,
            [FromBody] PatientUpdatingVM patient)
        {
            await sender.Send(new UpdatePatientCommand(
                patientId,
                mapper.Map<PatientDTO>(patient)));

            return Ok();
        }
    }
}
