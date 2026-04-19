using AgdtTestTask.Core.DataAccess.Abstracts;
using AgdtTestTask.Core.Mapping.Extensions;
using AgdtTestTask.Medical.BusinessLogic.Abstracts;
using AgdtTestTask.Medical.BusinessLogic.Abstracts.DTO;
using AgdtTestTask.Medical.Entities;
using AgdtTestTask.Medical.Fhir.Extensions;
using AutoMapper;

namespace AgdtTestTask.Medical.BusinessLogic
{
    internal sealed class PatientSearchingService(
        IRepository<Patient> patientRepository,
        IMapper mapper)
        : IPatientSearchingService
    {
        public async Task<PatientDTO> GetByNameGuidAsync(Guid guid)
        {
            var patient = await patientRepository.FirstOrDefaultAsync(
                x => x.Name.Id == guid);

            if (patient != null)
            {
                return mapper.Map<PatientDTO>(patient);
            }

            return null;
        }

        public async Task<IEnumerable<PatientDTO>> GetByBirthdateAsync(
            IEnumerable<string> birthdateParams)
        {
            var patients = await patientRepository.ApplyQueryAsync(
                x => x.BuildDateSearchExpression(birthdateParams, p => p.Birthdate));

            return mapper.MapCollection<PatientDTO>(patients);
        }
    }
}
